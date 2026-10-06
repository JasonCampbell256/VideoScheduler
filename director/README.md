# Signal — AI Programming Director proof of concept

Run with Node 22+ (no dependency installation required):

```sh
cd director
npm start
# http://localhost:5051
npm test
npm run build
```

The station starts **empty**. It does not populate example shows, episodes, ShowRuns, history or schedules. Legacy records marked `demo: true` are removed from saved state on startup; real media is retained. Synthetic fixtures exist only in tests and are never imported by the application.

`build` checks executable JavaScript syntax; browser assets are served directly. The original .NET app remains independently runnable.

## Load your real library

`STATION_LIBRARY` names an initial full station JSON snapshot. It is used only if there is no persisted state at `director/data/station.json` (or the path specified by `STATION_STATE`). To initialize from real records without replacing an existing station, select a new state path:

```sh
STATION_LIBRARY=/path/to/real-station.json STATION_STATE=/path/to/new-station-state.json npm start
```

The snapshot shape is defined in `state.mjs`: `revision`, `library` (`shows`, `assets`, `groups`), `runs`, `schedule`, `history`, and `playback`. Assets require canonical `id`, `title`, `type` (`episode`, `bumper`, `commercial`, `filler`), measured integer `durationSec`, `filePath`, and `tags`. Episodes also reference `showId`, `season`, `number` and optional `mature`. Shows contain ordered `episodes` (asset IDs), and runs reference `showId` and `nextIndex`. Schedule/history can start empty; playback starts with `currentId: null`, `positionSec: 0`, `status: "idle"`, `completed: []`.

Media file existence is always checked during resolution and apply. No flag bypasses this check. Durations are trusted catalog measurements, never model output. There is no scanner, legacy YAML importer or import screen yet, so importing existing scanned records currently requires mapping them into this JSON shape. An API key alone does not load your media.

## Configure the real AI Director

Set `OPENAI_API_KEY` in the server environment; optionally set `OPENAI_MODEL` (default `gpt-4.1-mini`). The server calls OpenAI Chat Completions through `library`, `runs`, `schedule` and `propose_rotation` tools. It requires all three read tools before a proposal. The model never receives a write tool. Without a key, the UI says **AI NOT CONFIGURED**, disables generation, and the service rejects Director requests rather than simulating a response.

The key stays on the server. This adapter has not been exercised against a live provider here. Station metadata returned by the read tools is sent to the configured OpenAI model.

## Programming flow

After loading real assets and ShowRuns and configuring the key:

1. Choose a future start, block length and maturity threshold; enter a programming brief.
2. Generate a preview. Inspect the timeline, resolved episodes, rationale and validation.
3. Apply. Persistence changes, but ShowRun progression does not.
4. Lock an item; regenerate the same block. Its identity, timing and media remain unchanged.

Time/repeat controls are authoritative. Natural language affects creative rotation; arbitrary prose constraints are not parsed into additional deterministic rules in this slice.

Real playback is not connected. The UI disables transport controls and reports nothing playing rather than presenting simulated playback as real. The HTTP service rejects playback commands until a real adapter is implemented. Completion/progression logic remains covered by domain tests for that future integration.

The service binds loopback only. It is a single-process personal prototype without authentication; do not expose it publicly. `PORT` defaults to 5051. State is atomically replaced and ignored by git.

## Verification

`npm test` includes 16 passing tests covering empty startup, persistent synthetic-data cleanup, preservation of mixed real data, mandatory file checks, unconfigured AI, HTTP empty states, exact duration, progression, maturity, repeats, protected items, stale revisions, persistence and idempotent completion. `npm run build` checks syntax.

Optional browser smoke (requires Playwright + Chromium and a fresh empty server):

```sh
node scripts/browser-smoke.cjs
```

It verifies empty controls, library/run navigation and mobile overflow and saves screenshots to `docs/`. Browser execution remains unverified here because Chromium is not installed locally and the cloud browser could not connect.

See [architectural decisions](docs/architecture.md) for domain reuse, boundaries and next steps.
