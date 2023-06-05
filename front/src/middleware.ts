import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function middleware(request: NextRequest) {
  if (
    request.nextUrl.pathname.startsWith("/admin") &&
    !request.nextUrl.pathname.startsWith("/admin/login")
  ) {
    if (!request.cookies.has("_digitalPetitionsAuth")) {
      return NextResponse.redirect(new URL("/admin/login", request.url), 307);
    }
  }
}

export const config = {
  matcher: "/admin/:path*",
};
