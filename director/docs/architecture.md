# Architecture decisions

## 1. Preserve the domain, replace the implementation boundary

Inspected baseline: `53ca734`, .NET 8 Blazor, YAML repository, VLC HTTP playback. `Data/Models/Asset`, `Episode`, `Show`, `ShowRun`, `LibraryData`, `PlaylistItem` and `AppStateData` supply the useful concepts. `Services/LibraryScanner` establishes canonical categories, measured durations and season/episode ordering. `SchedulePlanner` simulates run cursors for forecasts and preserves a current block. `AutomationService` advances runs on playback start.

The new slice preserves library categories/groups, canonical media IDs, ordered episodes, independent ShowRuns, concrete timed playlist items, bumpers/commercials/filler, history and playback state. It uses an explicit next episode index instead of legacy `CurrentEpisodeId`; an eventual importer must map it carefully. It advances on **confirmed completion**, rather than a VLC start command, to avoid treating a failed/aborted play as consumed. First/last run bounds, shuffle queues and weekly templates are deferred, not silently migrated.

A separate Node application in `director/` makes the new domain independent of the mutable singleton YAML repository, UI framework and VLC process. Node is available in this environment; .NET is not. This is an architecture experiment, not a claim that Node must replace .NET in the final product. The legacy app is left intact as a reference and potential source of scanner/VLC adapters.

## 2. AI supplies creative intent; deterministic services own facts

`ai.mjs` exposes three read tools and a proposal tool. The proposal contains run ordering, style and rationale, not media paths, episode IDs, durations or schedule writes. The service resolves next episodes from catalog/run state and inserts matching bumpers, then fills remaining time with whole available filler/commercial assets. It fails on an unfillable gap instead of trimming media or inventing duration. Episode repeats are avoided against a seven-day history window; promos/filler may repeat. Mature content is checked against an explicit timestamp watershed, so crossing midnight is handled naturally.

The allocator is deliberately simple: rotate through eligible runs and greedily fill gaps. It does not optimize cinematic pacing or globally solve constrained programming. Natural language affects creative ordering through the live AI adapter. Missing API configuration is an explicit error, with no demo fallback. Application controls express supported hard constraints; prose alone is not a deterministic constraint definition.

## 3. Preview is an isolated transaction proposal

A preview stores resolved immutable items in a server-owned map and captures the station revision. Apply accepts only its ID, retrieves the original server preview, rechecks media/constraints and rejects changed revisions or already-started blocks. Apply cannot receive a client-forged timeline. Preview/apply do not mutate run progression. All scheduled uncompleted episodes reserve progression; earlier blocks are considered during generation and the combined schedule is checked before commit, protecting later reservations from an earlier replacement.

Locks, current items and past programming are protected during regeneration. A request crossing a protected item boundary fails. Protected items are copied exactly. A lock or transport change increments the revision, invalidating old previews. Preview storage is bounded to 30 entries and intentionally lost on restart.

## 4. Persistence and transport are narrow ports

The station commits a full JSON snapshot via temporary-file rename before replacing in-memory state. This keeps schedule, runs, history and playback changes together. It supports one local writer, not distributed concurrency or durable audit logs. Crash recovery/fsync, schema migrations and database transactions remain future work.

Playback transition logic exists in the domain and is exercised in tests, but the production UI and HTTP playback commands are disabled until a real adapter is connected. Scheduled start times describe programming intent, not an active video output. Completion logic is guarded against duplicate consumption of an item; a real adapter must supply completion acknowledgments, identity and reconciliation after reconnect/restart.

## Known gaps / next steps

1. Add a legacy YAML importer with run range/shuffle semantics and fixtures; reuse scanner/ffprobe measurement behind an adapter.
2. Connect VLC or browser playback, using idempotent completion events, offset tracking and clock reconciliation.
3. Add provider contract tests, prompt-to-structured-constraints review and editable creative proposals; live AI is currently unverified.
4. Replace greedy gap filling with exact-fit search and richer bumper placement, commercial policies and show diversity scoring.
5. Add recurring schedule windows/templates, multiple stations, richer timeline interactions and per-block revision/transaction scope.
6. Add schema validation/migrations, retention, transactional storage and authentication before remote hosting.
7. Run the optional browser smoke on installed Chromium, capture screenshots and expand accessibility verification.

## Empty startup and real-data boundary

The station no longer seeds synthetic records. `state.mjs` defines an empty default and removes persisted legacy assets marked `demo: true`, along with their show/run/group/schedule/history references. Mixed libraries preserve real assets and adjust relative run positions when synthetic episodes are removed. The cleanup is persisted atomically. Synthetic fixtures are isolated under `test/fixtures` and not reachable through server startup. File existence checks now apply to every resolved asset without a demo bypass.

The UI shows empty library, ShowRuns, schedule and Now Playing states, disables generation until real media/runs and API configuration are present, and labels missing AI configuration explicitly. The playback UI and HTTP commands are disabled until a real transport adapter exists, so simulated playback cannot appear as real station status; real playback and media import UI remain next steps. Real station JSON initialization through `STATION_LIBRARY` is supported now.
