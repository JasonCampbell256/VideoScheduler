const $ = (id) => document.getElementById(id);
let state,
  preview,
  filter = "all";
const escape = (s) =>
  String(s).replace(
    /[&<>"']/g,
    (c) =>
      ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" })[
        c
      ],
  );
const time = (t) =>
  new Date(t).toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
const local = (d) =>
  new Date(d.getTime() - d.getTimezoneOffset() * 60000)
    .toISOString()
    .slice(0, 16);
let tonight = new Date();
tonight.setHours(20, 0, 0, 0);
if (tonight < Date.now()) tonight.setDate(tonight.getDate() + 1);
$("start").value = local(tonight);
$("mature").value = local(new Date(+tonight + 2 * 3600000));
async function api(path, data) {
  const r = await fetch(
    `/api/${path}`,
    data
      ? {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(data),
        }
      : {},
  );
  const body = await r.json();
  if (!r.ok) throw Error(body.error);
  return body;
}
function notify(message) {
  $("notice").textContent = message;
}
const asset = (id) => state.library.assets.find((a) => a.id === id);
function render() {
  const current = state.playbackConnected
    ? state.schedule.find((i) => i.id === state.playback.currentId)
    : null;
  $("mode").textContent =
    state.directorMode === "openai" ? "OPENAI DIRECTOR" : "AI NOT CONFIGURED";
  const hasLibrary = state.library.assets.length > 0;
  $("generate").disabled =
    !hasLibrary || !state.runs.length || state.directorMode !== "openai";
  $("play").disabled = !state.playbackConnected || !state.schedule.length;
  $("pause").disabled = !state.playbackConnected || !current;
  $("complete").disabled = !state.playbackConnected || !current;
  $("setup-note").hidden = hasLibrary;
  $("connection-status").textContent = "Local service connected";
  $("now-title").textContent = current
    ? asset(current.assetId).title
    : "Nothing playing.";
  $("now-detail").textContent = current
    ? `${state.playback.status.toUpperCase()} · ${Math.round(current.durationSec / 60)} min`
    : "Real playback is not connected in this version.";
  const upcoming = state.schedule.filter(
    (i) => i.id !== current?.id && !state.playback.completed.includes(i.id),
  );
  $("upnext").innerHTML =
    upcoming
      .slice(0, 3)
      .map(
        (i, n) =>
          `<div class="next-item"><span class="number">0${n + 1}</span><div><b>${escape(asset(i.assetId).title)}</b><small>${time(i.start)} · ${asset(i.assetId).type.toUpperCase()} · ${i.durationSec}s</small></div></div>`,
      )
      .join("") || "<p>No programming scheduled.</p>";
  const items = preview?.items || state.schedule;
  $("schedule-status").textContent = preview
    ? "PREVIEW / NOT APPLIED"
    : state.schedule.length
      ? "APPLIED / REV " + state.revision
      : "NO BLOCK APPLIED";
  const total = items.reduce((n, i) => n + i.durationSec, 0);
  $("timeline").innerHTML =
    items
      .map(
        (i) =>
          `<button class="${asset(i.assetId).type} ${i.locked ? "locked" : ""}" style="flex:${i.durationSec}" title="${escape(asset(i.assetId).title)} · ${time(i.start)}" aria-label="${escape(asset(i.assetId).title)}"><span>${i.durationSec > 300 ? escape(asset(i.assetId).title.split(" · ")[0]) : ""}</span></button>`,
      )
      .join("") || '<p style="padding:12px">No programming scheduled.</p>';
  $("schedule-list").innerHTML =
    `<div class="scroll-list">${items.map((i) => `<div class="schedule-row"><small>${time(i.start)}</small><b>${escape(asset(i.assetId).title)}</b><small>${i.durationSec / 60} min</small>${preview ? `<small>${i.locked ? "⌑ LOCKED" : "DRAFT"}</small>` : `<button data-lock="${i.id}" ${i.id === current?.id ? "disabled" : ""}>${i.id === current?.id ? "ACTIVE" : i.locked ? "Unlock" : "Lock"}</button>`}</div>`).join("")}</div>${total ? `<p class="note">${(total / 3600).toFixed(2)} hours · ${items.length} items · ${preview ? "Preview only" : "Committed schedule"}</p>` : ""}`;
  renderLibrary();
  $("run-grid").innerHTML =
    state.runs
      .map((r) => {
        const s = state.library.shows.find((s) => s.id === r.showId);
        return `<article class="media-card"><div class="media-art" style="--accent:${s.color}">↗</div><h3>${escape(r.name)}</h3><p>Next: ${escape(asset(s.episodes[r.nextIndex])?.title || "Run completed")}</p><p>${r.nextIndex} of ${s.episodes.length} episodes progressed</p></article>`;
      })
      .join("") ||
    "<p>No ShowRuns yet. Load your real library and runs to continue episodes.</p>";
}
function renderLibrary() {
  const q = $("search").value.toLowerCase();
  $("library-grid").innerHTML =
    state.library.assets
      .filter(
        (a) =>
          (filter === "all" || a.type === filter) &&
          a.title.toLowerCase().includes(q),
      )
      .map(
        (a) =>
          `<article class="media-card"><div class="media-art" style="--accent:${state.library.shows.find((s) => s.id === a.showId)?.color || "#477b80"}">${a.type === "episode" ? "◈" : "⏵"}</div><span class="eyebrow">${a.type.toUpperCase()} ${a.mature ? " / MATURE" : ""}</span><h3>${escape(a.title)}</h3><p>${a.durationSec}s · Local media · ${escape(a.tags.join(", "))}</p></article>`,
      )
      .join("") ||
    "<p>No media found. Load your real library to see your shows, bumpers and commercials here.</p>";
}
async function refresh() {
  state = await api("station");
  render();
}
function clearPreview() {
  preview = null;
  $("preview").innerHTML = "";
}
$("generate").onclick = async () => {
  const b = $("generate");
  b.disabled = true;
  notify(
    "Director is inspecting the library and building a validated preview…",
  );
  try {
    const start = +new Date($("start").value);
    preview = await api("preview", {
      prompt: $("prompt").value,
      start,
      end: start + Number($("hours").value) * 3600000,
      matureAfter: +new Date($("mature").value),
      avoidRepeats: $("repeats").checked,
    });
    $("preview").innerHTML =
      `<div class="validation"><b>${preview.validation.valid ? "✓ READY FOR REVIEW" : "VALIDATION FAILED"}</b><br>${preview.validation.checks.map((c) => escape(c)).join(" · ")}${preview.validation.errors.map((e) => `<p>${escape(e)}</p>`).join("")}</div><p>${escape(preview.rationale)}</p><div class="preview-actions"><button class="primary" id="apply" ${preview.validation.valid ? "" : "disabled"}>Apply this block</button><button id="discard">Discard</button></div>`;
    $("apply").onclick = async () => {
      try {
        await api("apply", { id: preview.id });
        clearPreview();
        await refresh();
        notify(
          "Block applied. ShowRun progression is unchanged until playback completion.",
        );
      } catch (e) {
        notify(e.message);
      }
    };
    $("discard").onclick = () => {
      clearPreview();
      render();
      notify("Preview discarded.");
    };
    render();
    notify("Preview ready. Review the timeline before applying.");
  } catch (e) {
    notify(e.message);
  } finally {
    b.disabled =
      !state?.library.assets.length ||
      !state?.runs.length ||
      state?.directorMode !== "openai";
  }
};
for (const [id, action] of [
  ["play", "play"],
  ["pause", "pause"],
  ["complete", "complete"],
])
  $(id).onclick = async () => {
    try {
      await api("playback", { action });
      clearPreview();
      await refresh();
      notify(`Transport: ${action}.`);
    } catch (e) {
      notify(e.message);
    }
  };
$("schedule-list").onclick = async (e) => {
  const id = e.target.dataset.lock;
  if (!id) return;
  try {
    await api("lock", { id });
    clearPreview();
    await refresh();
    notify("Protection updated. Existing previews are now stale.");
  } catch (e) {
    notify(e.message);
  }
};
document.querySelectorAll("[data-nav]").forEach(
  (b) =>
    (b.onclick = () => {
      document
        .querySelectorAll("[data-nav]")
        .forEach((n) => n.classList.toggle("active", n === b));
      for (const id of ["control", "library", "runs"])
        $(id).hidden = id !== b.dataset.nav;
    }),
);
document.querySelectorAll("[data-filter]").forEach(
  (b) =>
    (b.onclick = () => {
      filter = b.dataset.filter;
      document
        .querySelectorAll("[data-filter]")
        .forEach((n) => n.classList.toggle("active", n === b));
      renderLibrary();
    }),
);
$("search").oninput = renderLibrary;
refresh().catch((e) => {
  $("connection-status").textContent = "Local service unavailable";
  notify(e.message);
});
