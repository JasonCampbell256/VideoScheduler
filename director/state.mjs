export function emptyState() {
  return {
    revision: 0,
    library: { shows: [], assets: [], groups: [] },
    runs: [],
    schedule: [],
    history: [],
    playback: {
      currentId: null,
      positionSec: 0,
      status: "idle",
      completed: [],
    },
  };
}

// Remove only legacy synthetic records. Real media and its relative run position survive.
export function removeSyntheticData(source) {
  const state = structuredClone(source);
  const fakeIds = new Set(
    state.library.assets.filter((a) => a.demo === true).map((a) => a.id),
  );
  if (!fakeIds.size) return { state, changed: false };
  state.library.assets = state.library.assets.filter((a) => !fakeIds.has(a.id));
  const realIds = new Set(state.library.assets.map((a) => a.id));
  const oldShows = new Map(state.library.shows.map((s) => [s.id, s]));
  state.library.shows = state.library.shows
    .map((s) => ({
      ...s,
      episodes: s.episodes.filter((id) => realIds.has(id)),
    }))
    .filter((s) => s.episodes.length);
  const showIds = new Set(state.library.shows.map((s) => s.id));
  state.runs = state.runs
    .filter((r) => showIds.has(r.showId))
    .map((r) => ({
      ...r,
      nextIndex: oldShows
        .get(r.showId)
        .episodes.slice(0, r.nextIndex)
        .filter((id) => realIds.has(id)).length,
    }));
  state.library.groups = state.library.groups
    .map((g) => ({
      ...g,
      assetIds: g.assetIds.filter((id) => realIds.has(id)),
    }))
    .filter((g) => g.assetIds.length);
  const removedItems = new Set(
    state.schedule.filter((i) => !realIds.has(i.assetId)).map((i) => i.id),
  );
  state.schedule = state.schedule.filter((i) => realIds.has(i.assetId));
  state.history = state.history.filter((h) => realIds.has(h.assetId));
  state.playback.completed = state.playback.completed.filter(
    (id) => !removedItems.has(id),
  );
  if (removedItems.has(state.playback.currentId)) {
    state.playback.currentId = null;
    state.playback.positionSec = 0;
    state.playback.status = "idle";
  }
  return { state, changed: true };
}
