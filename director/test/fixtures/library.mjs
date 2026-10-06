export function seed() {
  const shows = [
    ["orbit", "Orbital Academy", false, "#8d7aff"],
    ["dragon", "Dragon Circuit", false, "#36c9ae"],
    ["ghost", "Ghost Protocol", true, "#ef877a"],
    ["night", "Midnight Ronin", true, "#d5a3fa"],
  ].map(([id, title, mature, color]) => ({
    id,
    title,
    mature,
    color,
    tags: ["toonami", "action"],
    episodes: Array.from({ length: 12 }, (_, i) => `${id}-${i + 1}`),
  }));
  const assets = shows.flatMap((s) =>
    s.episodes.map((id, i) => ({
      id,
      title: `${s.title} · Episode ${i + 1}`,
      showId: s.id,
      season: 1,
      number: i + 1,
      type: "episode",
      durationSec: 1380,
      filePath: new URL(import.meta.url).pathname,
      tags: s.tags,
      mature: s.mature,
    })),
  );
  assets.push(
    {
      id: "bumper",
      title: "Transmission / Toonami ident",
      type: "bumper",
      durationSec: 30,
      tags: ["toonami"],
      filePath: new URL(import.meta.url).pathname,
    },
    {
      id: "commercial",
      title: "Station promo",
      type: "commercial",
      durationSec: 30,
      tags: ["toonami"],
      filePath: new URL(import.meta.url).pathname,
    },
    {
      id: "filler",
      title: "Deep space interlude",
      type: "filler",
      durationSec: 60,
      tags: ["toonami"],
      filePath: new URL(import.meta.url).pathname,
    },
  );
  return {
    revision: 0,
    library: {
      shows,
      assets,
      groups: [
        {
          id: "toonami",
          name: "Toonami rotation",
          assetIds: assets.map((a) => a.id),
        },
      ],
    },
    runs: shows.map((s) => ({
      id: `run-${s.id}`,
      showId: s.id,
      name: `${s.title} / first run`,
      nextIndex: 2,
    })),
    schedule: [],
    history: [{ assetId: "orbit-2", at: Date.now() }],
    playback: {
      currentId: null,
      positionSec: 0,
      status: "idle",
      completed: [],
    },
  };
}
