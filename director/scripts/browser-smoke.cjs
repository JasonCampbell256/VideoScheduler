// Optional browser smoke: node scripts/browser-smoke.cjs (requires Playwright).
const { chromium } = require("playwright");
(async () => {
  const browser = await chromium.launch({ headless: true });
  const page = await browser.newPage({
    viewport: { width: 1440, height: 1100 },
  });
  const errors = [];
  page.on("pageerror", (e) => errors.push(e.message));
  await page.goto("http://127.0.0.1:5051");
  await page.getByRole("button", { name: "Generate preview" }).click();
  await page.getByRole("button", { name: "Apply this block" }).waitFor();
  await page.screenshot({
    path: "docs/control-room-preview.png",
    fullPage: true,
  });
  await page.getByRole("button", { name: "Apply this block" }).click();
  await page.waitForFunction(() =>
    document.getElementById("notice").textContent.startsWith("Block applied"),
  );
  await page.getByRole("button", { name: "Lock", exact: true }).first().click();
  await page.waitForFunction(
    () => document.querySelector("[data-lock]")?.textContent === "Unlock",
  );
  await page.getByRole("button", { name: "Play", exact: true }).click();
  await page.waitForFunction(() =>
    document.getElementById("now-detail").textContent.includes("PLAYING"),
  );
  await page.getByRole("button", { name: "Complete item" }).click();
  await page.getByRole("button", { name: "Media library" }).click();
  await page.locator("#search").fill("Ghost");
  if ((await page.locator(".media-card:visible").count()) !== 12)
    throw Error("Library search failed");
  await page.getByRole("button", { name: "Show runs" }).click();
  if ((await page.locator("#run-grid .media-card").count()) !== 4)
    throw Error("Runs missing");
  await page.setViewportSize({ width: 390, height: 844 });
  await page.getByRole("button", { name: "Control room" }).click();
  await page.screenshot({
    path: "docs/control-room-mobile.png",
    fullPage: true,
  });
  if (
    await page.evaluate(() => document.documentElement.scrollWidth > innerWidth)
  )
    throw Error("Mobile overflow");
  if (errors.length) throw Error(errors.join("; "));
  console.log(
    "Browser smoke passed: preview, apply, lock, playback, library, runs, mobile; no JS errors.",
  );
  await browser.close();
})().catch((e) => {
  console.error(e);
  process.exit(1);
});
