# Signal — AI Programming Director proof of concept

Run with Node 22+ (no dependency installation required):

```sh
cd director
npm start
# http://localhost:5051
npm test
npm run build
```

`build` checks executable JavaScript syntax; the browser assets are served directly, so there is no compilation step. The original .NET app remains separately runnable; this prototype does not invoke it.

## Try the vertical slice

1. Open the control room. Choose a future block start, length and maturity threshold.
2. Generate preview. Inspect the proposed timeline, resolved episodes, rationale and validation.
3. Apply. The service persists the schedule but does not advance ShowRuns.
4. Lock an item; regenerate the same block. Its identity, timing and media remain unchanged.
5. Play / Pause / Complete item operates a **simulated transport**. Completion records history and advances the episode's run exactly once.
6. Browse searchable media categories and inspect ShowRuns.

The seeded titles and assets are synthetic metadata. No video files are bundled and no real playback occurs. Demo mode is a fixed programming policy; it does not interpret free text. Start/time/repeat controls are authoritative in both modes. Arbitrary prose constraints are not parsed into new deterministic rules in this slice.

## Real AI mode

Set `OPENAI_API_KEY` in the server environment before starting; optionally set `OPENAI_MODEL` (default `gpt-4.1-mini`). The server calls OpenAI Chat Completions with explicit `library`, `runs`, `schedule` and `propose_rotation` tools. It requires the model to inspect all three read tools before proposing existing run IDs, bumper style and rationale. The model never receives a write tool. Provider errors are surfaced; there is no silent fallback to demo mode.

The API key stays on the server. This adapter has not been exercised against a live provider in this environment. All station metadata returned by those tools is sent to the configured OpenAI model; do not load sensitive metadata unintentionally.

## Persistence / local media

State is atomically replaced at `director/data/station.json`, ignored by git. `STATION_STATE` overrides its path. `STATION_LIBRARY` optionally names an initial full station JSON snapshot in the same shape as `seed.mjs`; it is used only if there is no persisted state. Existing scanned assets can be mapped to this schema with their IDs, file paths, measured `durationSec`, ordered show episode IDs and run positions. For real assets remove `demo` or set it false: resolution and apply check that the file exists. Durations are trusted catalog measurements, not model output. There is no scanner, YAML importer or duration probing in this slice.

The service binds loopback only. It is a single-process personal prototype with no authentication; do not expose it publicly. `PORT` defaults to 5051.

## Validation

Domain tests cover exact duration, episode ordering, watershed, repeats, preview isolation, immutable locks/current items, stale revisions, media rechecks, persistence, idempotent completion, boundary conflicts and adjacent-block reservations. `npm run build` validates syntax. An optional browser smoke script is provided:

```sh
# With Playwright + Chromium available, and a fresh demo server running:
node scripts/browser-smoke.cjs
```

It exercises preview/apply, locks, transport, library search, runs and mobile overflow; it writes screenshots to `docs/`. Browser execution was blocked here by missing local Chromium and an unreachable local server from the cloud browser. No screenshots or browser-pass claim are included.

See [architectural decisions](docs/architecture.md) for the legacy assessment and boundaries.
