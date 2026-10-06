import { test } from "node:test";
import assert from "node:assert/strict";
import { mkdtempSync, rmSync } from "node:fs";
import { tmpdir } from "node:os";
import { join } from "node:path";
import { Station } from "../domain.mjs";
import { seed } from "./fixtures/library.mjs";
import { direct } from "../ai.mjs";
const request = () => ({
  prompt: "Toonami tonight",
  start: Date.now() + 3600000,
  end: Date.now() + 3600000 + 14400000,
  matureAfter: Date.now() + 10800000,
  avoidRepeats: true,
});
const proposal = (s) => ({
  runIds: s.state.runs.map((r) => r.id),
  style: "toonami",
  mode: "demo",
});
const make = (s) => {
  const r = request();
  r.end = r.start + 14400000;
  r.matureAfter = r.start + 7200000;
  return s.preview(r, proposal(s));
};
test("exact four-hour block, ordered episodes, mature later, no preview/apply progression", () => {
  const s = new Station({ initial: seed() });
  const before = structuredClone(s.state.runs);
  const p = make(s);
  assert.equal(p.validation.valid, true, p.validation.errors.join(","));
  assert.equal(
    p.items.reduce((n, i) => n + i.durationSec, 0),
    14400,
  );
  assert.deepEqual(s.state.runs, before);
  for (const i of p.items)
    if (s.asset(i.assetId).mature) assert.ok(i.start >= p.request.matureAfter);
  s.apply(p.id);
  assert.deepEqual(s.state.runs, before);
});
test("stale preview cannot apply after a lock mutation", () => {
  const s = new Station({ initial: seed() });
  s.apply(make(s).id);
  const p = make(s);
  s.lock(s.state.schedule[0].id);
  assert.throws(() => s.apply(p.id), /changed/);
});
test("regeneration preserves locked identity, timing and payload", () => {
  const s = new Station({ initial: seed() });
  const first = make(s);
  s.apply(first.id);
  const item = s.state.schedule.find((i) => i.runId);
  s.lock(item.id);
  const p = s.preview(first.request, proposal(s));
  assert.deepEqual(
    p.items.find((i) => i.id === item.id),
    s.state.schedule.find((i) => i.id === item.id),
  );
  assert.ok(p.validation.valid);
  s.apply(p.id);
});
test("missing file, unknown run, unknown asset and forged duration rejected", () => {
  const s = new Station({ initial: seed() });
  s.state.library.assets.find((a) => a.id === "orbit-3").filePath =
    "/nonexistent/signal-test-media";
  assert.throws(() => make(s), /Missing media/);
  assert.throws(
    () => s.preview(request(), { runIds: ["fake"] }),
    /existing runs/,
  );
  assert.throws(() => s.asset("fake"), /Unknown media/);
  const other = new Station({ initial: seed() });
  const p = make(other);
  p.items[0].durationSec++;
  assert.equal(other.validate(p.items, p.request).valid, false);
});
test("completion advances exactly once, persists atomically across restart", () => {
  const dir = mkdtempSync(join(tmpdir(), "signal-"));
  try {
    const file = join(dir, "station.json");
    const s = new Station({ file, initial: seed() });
    s.apply(make(s).id);
    s.playback("play");
    s.playback("complete");
    s.playback("play");
    const run = s.state.runs.find((r) => r.id === s.state.schedule[1].runId);
    const old = run.nextIndex;
    s.playback("complete");
    assert.equal(s.state.runs.find((r) => r.id === run.id).nextIndex, old + 1);
    assert.throws(() => s.playback("complete"), /No active/);
    assert.deepEqual(
      new Station({ file, initial: seed() }).snapshot(),
      s.snapshot(),
    );
  } finally {
    rmSync(dir, { recursive: true });
  }
});
test("recent next episode defers run without skipping progression", () => {
  const s = new Station({ initial: seed() });
  s.state.history.push({ assetId: "orbit-3", at: Date.now() });
  const p = make(s);
  assert.ok(
    !p.items.some((i) => i.assetId === "orbit-3" || i.assetId === "orbit-4"),
  );
  assert.ok(p.validation.valid);
});
test("unfillable gaps and protected boundary cuts fail", () => {
  const s = new Station({ initial: seed() });
  const r = request();
  r.end = r.start + 31 * 1000;
  assert.throws(() => s.preview(r, proposal(s)), /exactly fill/);
  const p = make(s);
  s.apply(p.id);
  s.lock(s.state.schedule[1].id);
  assert.throws(
    () =>
      s.preview(
        { ...p.request, start: s.state.schedule[1].start + 1000 },
        proposal(s),
      ),
    /boundary/,
  );
});
test("current item cannot be unlocked or mutated on regeneration", () => {
  const s = new Station({ initial: seed() });
  const p = make(s);
  s.apply(p.id);
  s.playback("play");
  const id = s.state.playback.currentId;
  assert.throws(() => s.lock(id), /always protected/);
  const next = s.preview(p.request, proposal(s));
  assert.deepEqual(
    next.items.find((i) => i.id === id),
    s.state.schedule.find((i) => i.id === id),
  );
});
test("adjacent block reserves episodes; out of order replacement cannot corrupt later block", () => {
  const s = new Station({ initial: seed() });
  const p = make(s);
  s.apply(p.id);
  const later = s.preview(
    {
      ...p.request,
      start: p.request.end,
      end: p.request.end + 14400000,
      matureAfter: p.request.start,
    },
    proposal(s),
  );
  assert.ok(later.validation.valid);
  s.apply(later.id);
  const replacement = s.preview(p.request, {
    ...proposal(s),
    runIds: ["run-dragon"],
  });
  assert.throws(() => s.apply(replacement.id), /progression/);
});
test("apply rechecks existence rather than trusting preview validation", () => {
  const s = new Station({ initial: seed() });
  const p = make(s);
  s.state.library.assets.find((a) => a.id === p.items[1].assetId).filePath =
    "/nonexistent/signal-test-media";
  assert.throws(() => s.apply(p.id), /Missing media/);
});
test("Director requires a real API key instead of simulating AI", async () => {
  const saved = process.env.OPENAI_API_KEY;
  delete process.env.OPENAI_API_KEY;
  try {
    await assert.rejects(
      () => direct(new Station({ initial: seed() }), request()),
      /Configure OPENAI_API_KEY/,
    );
  } finally {
    if (saved !== undefined) process.env.OPENAI_API_KEY = saved;
  }
});
