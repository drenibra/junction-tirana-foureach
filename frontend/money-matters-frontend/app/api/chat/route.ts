import { NextResponse } from "next/server";
import OpenAI from "openai";

export async function POST(request: Request) {
  try {
    const { messages } = await request.json();
    // Initialize OpenAI on the server only
    const openai = new OpenAI({ apiKey: process.env.OPENAI_API_KEY });
    const response = await openai.chat.completions.create({
      model: "gpt-4o-mini",
      messages,
    });
    const content = response.choices[0]?.message.content || "";
    return NextResponse.json({ content });
  } catch (err) {
    console.error("OpenAI error:", err);
    return NextResponse.json(
      { error: "Failed to fetch from OpenAI" },
      { status: 500 }
    );
  }
}
