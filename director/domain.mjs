import {
  existsSync,
  statSync,
  readFileSync,
  writeFileSync,
  renameSync,
  mkdirSync,
} from "node:fs";
import { dirname } from "node:path";
import { randomUUID } from "node:crypto";
import { seed } from "./seed.mjs";
export class Station {
  constructor({ file = null, initial = null } = {}) {
    this.file = file;
    this.state =
      file && existsSync(file)
        ? JSON.parse(readFileSync(file, "utf8"))
        : initial || seed();
    this.previews = new Map();
  }
  snapshot() {
    return structuredClone(this.state);
  }
  commit(next) {
    next.revision = this.state.revision + 1;
    if (this.file) {
      mkdirSync(dirname(this.file), { recursive: true });
      writeFileSync(`${this.file}.tmp`, JSON.stringify(next, null, 2));
      renameSync(`${this.file}.tmp`, this.file);
    }
    this.state = next;
    return this.snapshot();
  }
  asset(id) {
    const a = this.state.library.assets.find((a) => a.id === id);
    if (!a) throw Error(`Unknown media: ${id}`);
    if (!Number.isSafeInteger(a.durationSec) || a.durationSec <= 0)
      throw Error(`Invalid duration: ${id}`);
    if (
      !a.demo &&
      (!a.filePath || !existsSync(a.filePath) || !statSync(a.filePath).isFile())
    )
      throw Error(`Missing media: ${id}`);
    return a;
  }
  indicesBefore(start) {
    const indices = new Map(this.state.runs.map((r) => [r.id, r.nextIndex]));
    for (const i of this.state.schedule.filter(
      (i) =>
        i.start < start &&
        i.runId &&
        !this.state.playback.completed.includes(i.id),
    ))
      indices.set(i.runId, indices.get(i.runId) + 1);
    return indices;
  }
  tools() {
    return {
      library: () => structuredClone(this.state.library),
      runs: () => structuredClone(this.state.runs),
      schedule: () =>
        structuredClone({
          revision: this.state.revision,
          schedule: this.state.schedule,
          playback: this.state.playback,
          history: this.state.history,
        }),
    };
  }
  validate(items, request) {
    const errors = [];
    let cursor = request.start;
    const indices = this.indicesBefore(request.start);
    const seen = new Set(
      this.state.history
        .filter((h) => h.at >= request.now - 7 * 86400000)
        .map((h) => h.assetId),
    );
    const ids = new Set();
    for (const item of items) {
      try {
        const a = this.asset(item.assetId);
        if (ids.has(item.id)) throw Error("Duplicate schedule ID");
        ids.add(item.id);
        if (item.start !== cursor || item.durationSec !== a.durationSec)
          throw Error("Gap, overlap, or forged duration");
        if (item.start + a.durationSec * 1000 > request.end)
          throw Error("Block overrun");
        if (a.mature && item.start < request.matureAfter)
          throw Error("Mature programming before watershed");
        if (a.type === "episode") {
          const r = this.state.runs.find((r) => r.id === item.runId);
          const s = this.state.library.shows.find((s) => s.id === r?.showId);
          if (!s || s.episodes[indices.get(r.id)] !== a.id)
            throw Error("Episode progression mismatch");
          indices.set(r.id, indices.get(r.id) + 1);
          if (request.avoidRepeats && seen.has(a.id))
            throw Error("Recent episode repeat");
          seen.add(a.id);
        }
        cursor = item.start + a.durationSec * 1000;
      } catch (e) {
        errors.push(e.message);
      }
    }
    if (cursor !== request.end)
      errors.push("Block does not fill requested duration");
    return {
      valid: errors.length === 0,
      errors,
      totalSec: (cursor - request.start) / 1000,
      checks: [
        "Media availability",
        "Exact durations / no gaps",
        "Run continuity",
        "Recent repeats",
        "Maturity watershed",
        "Protected programming",
      ],
    };
  }
  preview(request, proposal) {
    const now = Date.now();
    request = { ...request, now };
    if (
      !Number.isFinite(request.start) ||
      !Number.isFinite(request.end) ||
      request.end <= request.start ||
      request.end - request.start > 8 * 3600000 ||
      request.start < now - 60000
    )
      throw Error("Choose a future block of at most eight hours");
    if (!Number.isFinite(request.matureAfter))
      throw Error("Invalid maturity threshold");
    if (
      !Array.isArray(proposal.runIds) ||
      !proposal.runIds.length ||
      proposal.runIds.length > 32 ||
      proposal.runIds.some((id) => !this.state.runs.some((r) => r.id === id))
    )
      throw Error("Director must select existing runs");
    const items = [];
    const indices = this.indicesBefore(request.start);
    let cursor = request.start;
    const protectedItems = this.state.schedule.filter(
      (i) =>
        i.locked || i.id === this.state.playback.currentId || i.start < now,
    );
    if (
      protectedItems.some(
        (i) =>
          (i.start < request.start &&
            i.start + i.durationSec * 1000 > request.start) ||
          (i.start < request.end &&
            i.start + i.durationSec * 1000 > request.end),
      )
    )
      throw Error("Block boundary cuts protected programming");
    const add = (a, runId = null, kept = null) => {
      const item = kept || {
        id: randomUUID(),
        assetId: a.id,
        start: cursor,
        durationSec: a.durationSec,
        runId,
        locked: false,
      };
      if (item.start !== cursor)
        throw Error("Protected programming conflicts with new timeline");
      items.push(structuredClone(item));
      cursor += a.durationSec * 1000;
      if (runId) indices.set(runId, indices.get(runId) + 1);
    };
    const recent = new Set(
      this.state.history
        .filter((h) => h.at > now - 7 * 86400000)
        .map((h) => h.assetId),
    );
    let turn = 0;
    while (cursor < request.end) {
      const kept = protectedItems.find((i) => i.start === cursor);
      if (kept) {
        add(this.asset(kept.assetId), kept.runId, kept);
        continue;
      }
      const boundary = Math.min(
        request.end,
        ...protectedItems
          .filter((i) => i.start > cursor && i.start < request.end)
          .map((i) => i.start),
      );
      const remaining = (boundary - cursor) / 1000;
      const eligible = proposal.runIds
        .map((id) => this.state.runs.find((r) => r.id === id))
        .filter((r) => {
          const s = this.state.library.shows.find((s) => s.id === r.showId);
          const a = this.state.library.assets.find(
            (a) => a.id === s?.episodes[indices.get(r.id)],
          );
          return (
            a &&
            a.durationSec + 30 <= remaining &&
            (!a.mature || cursor + 30000 >= request.matureAfter) &&
            (!request.avoidRepeats || !recent.has(a.id))
          );
        });
      if (eligible.length) {
        const r = eligible[turn++ % eligible.length];
        const s = this.state.library.shows.find((s) => s.id === r.showId);
        const bumper = this.state.library.assets.find(
          (a) =>
            a.type === "bumper" &&
            a.tags.includes(proposal.style || "toonami") &&
            a.durationSec === 30,
        );
        if (!bumper) throw Error("No matching 30-second bumper");
        add(this.asset(bumper.id));
        add(this.asset(s.episodes[indices.get(r.id)]), r.id);
      } else {
        const filler = this.state.library.assets
          .filter(
            (a) =>
              ["commercial", "filler"].includes(a.type) &&
              a.durationSec <= remaining,
          )
          .sort((a, b) => b.durationSec - a.durationSec)[0];
        if (!filler)
          throw Error(`Cannot exactly fill ${remaining}s with available media`);
        add(this.asset(filler.id));
      }
    }
    const validation = this.validate(items, request);
    for (const p of protectedItems.filter(
      (i) => i.start >= request.start && i.start < request.end,
    ))
      if (
        JSON.stringify(p) !== JSON.stringify(items.find((i) => i.id === p.id))
      )
        validation.errors.push("Protected item modified");
    validation.valid = validation.errors.length === 0;
    const preview = {
      id: randomUUID(),
      revision: this.state.revision,
      request,
      items,
      validation,
      rationale: String(
        proposal.rationale ||
          "Balanced action rotation; continue runs, with mature shows after the watershed.",
      ),
      mode: proposal.mode || "demo",
    };
    this.previews.set(preview.id, structuredClone(preview));
    if (this.previews.size > 30)
      this.previews.delete(this.previews.keys().next().value);
    return preview;
  }
  apply(id) {
    const p = this.previews.get(id);
    if (!p) throw Error("Preview expired");
    if (p.revision !== this.state.revision)
      throw Error("Station changed. Generate a fresh preview.");
    if (p.request.start < Date.now())
      throw Error("Block has started. Generate a fresh preview.");
    const v = this.validate(p.items, p.request);
    if (!v.valid) throw Error(v.errors.join("; "));
    const next = this.snapshot();
    next.schedule = next.schedule
      .filter((i) => i.start < p.request.start || i.start >= p.request.end)
      .concat(p.items)
      .sort((a, b) => a.start - b.start);
    if (
      next.schedule.some(
        (i, n) =>
          n &&
          next.schedule[n - 1].start + next.schedule[n - 1].durationSec * 1000 >
            i.start,
      )
    )
      throw Error("Conflicts with existing programming");
    const indices = new Map(next.runs.map((r) => [r.id, r.nextIndex]));
    for (const item of next.schedule.filter(
      (i) => i.runId && !next.playback.completed.includes(i.id),
    )) {
      const r = next.runs.find((r) => r.id === item.runId);
      const show = next.library.shows.find((s) => s.id === r?.showId);
      if (show?.episodes[indices.get(r?.id)] !== item.assetId)
        throw Error("Conflicts with reserved episode progression");
      indices.set(r.id, indices.get(r.id) + 1);
    }
    this.previews.delete(id);
    return this.commit(next);
  }
  lock(id) {
    const next = this.snapshot();
    const i = next.schedule.find((i) => i.id === id);
    if (!i) throw Error("Item not found");
    if (id === next.playback.currentId)
      throw Error("Current programming is always protected");
    i.locked = !i.locked;
    return this.commit(next);
  }
  playback(action) {
    const next = this.snapshot();
    const p = next.playback;
    const current = next.schedule.find((i) => i.id === p.currentId);
    if (action === "pause") {
      p.status = "paused";
    } else if (action === "play") {
      const item =
        current || next.schedule.find((i) => !p.completed.includes(i.id));
      if (!item) throw Error("Apply a schedule first");
      this.asset(item.assetId);
      p.currentId = item.id;
      p.status = "playing";
    } else if (action === "complete") {
      if (!current || p.completed.includes(current.id))
        throw Error("No active uncompleted item");
      if (current.runId) {
        const r = next.runs.find((r) => r.id === current.runId);
        const s = next.library.shows.find((s) => s.id === r.showId);
        if (s.episodes[r.nextIndex] !== current.assetId)
          throw Error("Playback progression conflict");
        r.nextIndex++;
      }
      p.completed.push(current.id);
      next.history.push({ assetId: current.assetId, at: Date.now() });
      p.currentId = null;
      p.positionSec = 0;
      p.status = "idle";
    } else throw Error("Unknown playback action");
    return this.commit(next);
  }
}
