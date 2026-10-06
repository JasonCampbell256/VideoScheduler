import http from "node:http";
import { readFileSync } from "node:fs";
import { fileURLToPath } from "node:url";
import { resolve } from "node:path";
import { Station } from "./domain.mjs";
import { direct } from "./ai.mjs";
const root = fileURLToPath(new URL("./public/", import.meta.url));
const station = new Station({
  file:
    process.env.STATION_STATE ||
    fileURLToPath(new URL("./data/station.json", import.meta.url)),
  initial: process.env.STATION_LIBRARY
    ? JSON.parse(readFileSync(process.env.STATION_LIBRARY, "utf8"))
    : null,
});
const port = Number(process.env.PORT || 5051);
export const server = http.createServer(async (req, res) => {
  try {
    if (req.method === "GET" && req.url === "/api/station") {
      res.setHeader("Content-Type", "application/json");
      res.end(
        JSON.stringify({
          ...station.snapshot(),
          directorMode: process.env.OPENAI_API_KEY
            ? "openai"
            : "not configured",
        }),
      );
      return;
    }
    if (req.method === "POST" && req.url.startsWith("/api/")) {
      if (
        req.headers.origin &&
        req.headers.origin !== `http://${req.headers.host}`
      )
        throw Error("Cross-origin mutation rejected");
      if (!String(req.headers["content-type"]).startsWith("application/json"))
        throw Error("JSON required");
      let body = "";
      for await (const chunk of req) {
        body += chunk;
        if (body.length > 16384) throw Error("Request too large");
      }
      const input = JSON.parse(body);
      let output;
      if (req.url === "/api/preview") {
        if (typeof input.prompt !== "string" || input.prompt.length > 4000)
          throw Error("Invalid prompt");
        if (!station.state.library.assets.length || !station.state.runs.length)
          throw Error(
            "Load real media and create ShowRuns before generating programming.",
          );
        const revision = station.state.revision;
        const proposal = await direct(station, input);
        if (revision !== station.state.revision)
          throw Error("Station changed during generation. Retry.");
        output = station.preview(input, proposal);
      } else if (req.url === "/api/apply") output = station.apply(input.id);
      else if (req.url === "/api/lock") output = station.lock(input.id);
      else if (req.url === "/api/playback")
        throw Error("Real playback is not connected in this version.");
      else throw Error("Unknown service");
      res.setHeader("Content-Type", "application/json");
      res.end(JSON.stringify(output));
      return;
    }
    if (req.method !== "GET") {
      res.writeHead(405);
      res.end();
      return;
    }
    const files = {
      "/": "index.html",
      "/app.js": "app.js",
      "/style.css": "style.css",
    };
    const file = files[req.url];
    if (!file) {
      res.writeHead(404);
      res.end();
      return;
    }
    res.setHeader(
      "Content-Type",
      file.endsWith(".css")
        ? "text/css"
        : file.endsWith(".js")
          ? "text/javascript"
          : "text/html",
    );
    res.end(readFileSync(resolve(root, file)));
  } catch (e) {
    res.writeHead(400, { "Content-Type": "application/json" });
    res.end(JSON.stringify({ error: e.message }));
  }
});
server.listen(port, "127.0.0.1", () =>
  console.log(`Broadcast station: http://localhost:${port}`),
);
