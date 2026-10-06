import { test } from "node:test";
import assert from "node:assert/strict";
import { spawn } from "node:child_process";
import { mkdtempSync, rmSync } from "node:fs";
import { tmpdir } from "node:os";
import { join } from "node:path";
import { once } from "node:events";
test("HTTP vertical slice and mutation boundary", async () => {
  const dir = mkdtempSync(join(tmpdir(), "signal-http-"));
  const child = spawn(process.execPath, ["server.mjs"], {
    cwd: new URL("../", import.meta.url),
    env: {
      ...process.env,
      PORT: "15051",
      STATION_STATE: join(dir, "station.json"),
      OPENAI_API_KEY: "",
      STATION_LIBRARY: "",
    },
  });
  try {
    await Promise.race([
      once(child.stdout, "data"),
      once(child, "error").then(([e]) => {
        throw e;
      }),
    ]);
    const post = async (path, data, headers = {}) => {
      const r = await fetch(`http://127.0.0.1:15051/api/${path}`, {
        method: "POST",
        headers: { "Content-Type": "application/json", ...headers },
        body: JSON.stringify(data),
      });
      return { status: r.status, body: await r.json() };
    };
    assert.equal((await fetch("http://127.0.0.1:15051")).status, 200);
    const start = Date.now() + 3600000;
    const p = await post("preview", {
      prompt: "Toonami",
      start,
      end: start + 14400000,
      matureAfter: start + 7200000,
      avoidRepeats: true,
    });
    assert.equal(p.status, 200);
    assert.ok(p.body.validation.valid);
    assert.equal(
      (
        await post(
          "apply",
          { id: p.body.id },
          { Origin: "https://untrusted.example" },
        )
      ).status,
      400,
    );
    assert.equal((await post("apply", { id: p.body.id })).status, 200);
    assert.equal((await post("apply", { id: p.body.id })).status, 400);
    assert.equal(
      (await post("playback", { action: "play" })).body.playback.status,
      "playing",
    );
    const done = await post("playback", { action: "complete" });
    assert.equal(done.body.history.length, 2);
    assert.equal((await post("playback", { action: "complete" })).status, 400);
  } finally {
    child.kill();
    await once(child, "exit");
    rmSync(dir, { recursive: true });
  }
});
