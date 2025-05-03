import { jwtVerify } from "jose";
import { NextRequest, NextResponse } from "next/server";

const secret = new TextEncoder().encode(process.env.APP_SECRET!);

export async function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl;

  // 1) Bypass static assets, public pages, AND your API routes:
  if (
    pathname.startsWith("/_next/") ||
    pathname.startsWith("/static/") ||
    pathname.startsWith("/images/") ||
    pathname.startsWith("/api/auth/") || // ← skip your login/register endpoints
    pathname === "/signup" ||
    pathname === "/questionnaire" ||
    pathname === "/login" ||
    pathname === "/favicon.ico"
  ) {
    return NextResponse.next();
  }

  // 2) All other requests require a valid JWT cookie
  const token = request.cookies.get("token")?.value;
  if (!token) {
    return NextResponse.redirect(new URL("/questionnaire", request.url));
  }

  try {
    const { payload } = await jwtVerify(token, secret);
    const response = NextResponse.next();
    response.headers.set("x-user-role", String(payload.role));
    return response;
  } catch {
    // delete bad cookie so it won’t loop
    const response = NextResponse.redirect(
      new URL("/questionnaire", request.url)
    );
    response.cookies.delete("token");
    return response;
  }
}

export const config = {
  matcher: ["/:path*"],
};
