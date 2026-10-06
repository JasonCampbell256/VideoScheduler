import { test } from "node:test";
import assert from "node:assert/strict";
import { mkdtempSync, writeFileSync, rmSync } from "node:fs";
import { tmpdir } from "node:os";
import { join } from "node:path";
import { Station } from "../domain.mjs";
import { emptyState } from "../state.mjs";
import { seed } from "./fixtures/library.mjs";

test("new stations start empty without fabricated media or history", () => {
  assert.deepEqual(new Station().snapshot(), emptyState());
});

test("persisted synthetic library is removed durably on startup", () => {
  const dir = mkdtempSync(join(tmpdir(), "signal-cleanup-"));
  try {
    const file = join(dir, "station.json");
    const fake = seed();
    for (const a of fake.library.assets) {
      a.demo = true;
      delete a.filePath;
    }
    fake.schedule = [
      { id: "fake-current", assetId: fake.library.assets[0].id },
    ];
    fake.playback = {
      currentId: "fake-current",
      positionSec: 20,
      status: "playing",
      completed: [],
    };
    writeFileSync(file, JSON.stringify(fake));
    const s = new Station({ file });
    const expected = emptyState();
    expected.revision = 1;
    assert.deepEqual(s.snapshot(), expected);
    assert.deepEqual(new Station({ file }).snapshot(), expected);
  } finally {
    rmSync(dir, { recursive: true });
  }
});

test("mixed libraries keep real assets, schedules, history and relative run progress", () => {
  const data = seed();
  const s = data.library.shows[0];
  const realId = s.episodes[2];
  data.library.assets
    .filter((a) => a.id !== realId)
    .forEach((a) => (a.demo = true));
  data.runs[0].nextIndex = 3;
  data.schedule = [{ id: "real-current", assetId: realId }];
  data.playback.currentId = "real-current";
  data.playback.status = "playing";
  data.history.push({ assetId: realId, at: 123 });
  const cleaned = new Station({ initial: data }).snapshot();
  assert.equal(cleaned.library.assets[0].id, realId);
  assert.equal(cleaned.library.assets.length, 1);
  assert.equal(cleaned.runs[0].nextIndex, 1);
  assert.equal(cleaned.runs.length, 1);
  assert.equal(cleaned.schedule[0].id, "real-current");
  assert.equal(cleaned.playback.currentId, "real-current");
  assert.deepEqual(cleaned.history, [{ assetId: realId, at: 123 }]);
});

test("demo flag cannot bypass actual file existence", () => {
  const s = new Station({ initial: seed() });
  const asset = s.state.library.assets[0];
  asset.demo = true;
  asset.filePath = "/nonexistent/media";
  assert.throws(() => s.asset(asset.id), /Missing media/);
});
