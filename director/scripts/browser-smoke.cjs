// Optional empty-state smoke: requires Playwright + Chromium and a fresh server.
const { chromium } = require("playwright");
(async () => {
  const browser = await chromium.launch({ headless: true });
  const page = await browser.newPage({
    viewport: { width: 1440, height: 1100 },
  });
  const errors = [];
  page.on("pageerror", (e) => errors.push(e.message));
  await page.goto("http://127.0.0.1:5051");
  await page.getByText("Your library is empty.", { exact: true }).waitFor();
  if (await page.getByRole("button", { name: "Generate preview" }).isEnabled())
    throw Error("Empty station allowed generation");
  if (await page.getByRole("button", { name: "Play", exact: true }).isEnabled())
    throw Error("Empty station allowed playback");
  await page.screenshot({
    path: "docs/control-room-empty.png",
    fullPage: true,
  });
  await page.getByRole("button", { name: "Media library" }).click();
  if (await page.locator(".media-card:visible").count())
    throw Error("Empty library has media cards");
  await page.getByRole("button", { name: "Show runs" }).click();
  if (await page.locator("#run-grid .media-card").count())
    throw Error("Empty station has runs");
  await page.setViewportSize({ width: 390, height: 844 });
  await page.getByRole("button", { name: "Control room" }).click();
  await page.screenshot({
    path: "docs/control-room-empty-mobile.png",
    fullPage: true,
  });
  if (
    await page.evaluate(() => document.documentElement.scrollWidth > innerWidth)
  )
    throw Error("Mobile overflow");
  if (errors.length) throw Error(errors.join("; "));
  console.log("Empty-state browser smoke passed.");
  await browser.close();
})().catch((e) => {
  console.error(e);
  process.exit(1);
});
