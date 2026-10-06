// The model has read-only tools and one proposal tool. It never receives a write capability.
export async function direct(station, request) {
  const read = station.tools();
  if (!process.env.OPENAI_API_KEY)
    throw Error("Configure OPENAI_API_KEY to enable the AI Director.");
  const functions = [
    ...["library", "runs", "schedule"].map((name) => ({
      type: "function",
      function: {
        name,
        description: `Read authoritative ${name}`,
        parameters: {
          type: "object",
          properties: {},
          additionalProperties: false,
        },
      },
    })),
    {
      type: "function",
      function: {
        name: "propose_rotation",
        description:
          "Propose creative run order and bumper style; deterministic scheduler resolves all episodes and durations.",
        parameters: {
          type: "object",
          properties: {
            runIds: { type: "array", items: { type: "string" } },
            style: { type: "string" },
            rationale: { type: "string" },
          },
          required: ["runIds", "style", "rationale"],
          additionalProperties: false,
        },
      },
    },
  ];
  const messages = [
    {
      role: "system",
      content:
        "You are a personal broadcast Programming Director. First read library, runs, and schedule using tools. Then propose_rotation with existing run IDs in a creative order matching the user request. You cannot change state or choose episode IDs, times, or durations. Constraints in the user request are enforced by the application. Do not claim a schedule was applied.",
    },
    { role: "user", content: JSON.stringify(request) },
  ];
  const consulted = new Set();
  for (let n = 0; n < 6; n++) {
    const response = await fetch("https://api.openai.com/v1/chat/completions", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${process.env.OPENAI_API_KEY}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        model: process.env.OPENAI_MODEL || "gpt-4.1-mini",
        messages,
        tools: functions,
        tool_choice: "required",
      }),
      signal: AbortSignal.timeout(30000),
    });
    if (!response.ok)
      throw Error(`Director provider returned ${response.status}`);
    const result = await response.json();
    const message = result.choices?.[0]?.message;
    if (!message?.tool_calls?.length) throw Error("Director did not use tools");
    messages.push(message);
    for (const call of message.tool_calls) {
      const name = call.function.name;
      if (name === "propose_rotation") {
        if (consulted.size !== 3)
          throw Error("Director must inspect station before proposing");
        return { ...JSON.parse(call.function.arguments), mode: "openai" };
      }
      if (!Object.hasOwn(read, name)) throw Error("Unrecognized Director tool");
      consulted.add(name);
      messages.push({
        role: "tool",
        tool_call_id: call.id,
        content: JSON.stringify(read[name]()),
      });
    }
  }
  throw Error("Director exceeded tool budget");
}
