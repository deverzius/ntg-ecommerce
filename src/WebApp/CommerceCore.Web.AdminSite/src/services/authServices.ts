import { dotenv } from "@/shared/constants/dotenv";
import type {
  AuthResponseDto,
  UserResponseDto,
} from "@/shared/types/dtos/auth/response";

export async function getUserInfo(): Promise<UserResponseDto> {
  return await fetch(`${dotenv.IDENTITY_SERVER_URL}/connect/userinfo`, {
    method: "GET",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
      Accept: "application/json",
    },
    credentials: "include",
  }).then((res) => res.json());
}

export async function authorizeUser(): Promise<Response> {
  return await fetch(`${dotenv.IDENTITY_SERVER_URL}/connect/authorize`, {
    method: "POST",
    headers: { "Content-Type": "application/x-www-form-urlencoded" },
    body: new URLSearchParams({
      client_id: dotenv.CLIENT_ID,
      client_secret: dotenv.CLIENT_SECRET,
      redirect_uri: `${dotenv.CLIENT_URL}/callback`,
      response_type: "code",
      grant_type: "authorization_code",
      scope: "openid offline_access",
    }),
  }).then((res) => res);
}

export async function logOutUser(): Promise<Response> {
  return await fetch(`${dotenv.IDENTITY_SERVER_URL}/connect/logout`, {
    method: "GET",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    credentials: "include",
  }).then((res) => res);
}

export async function fetchToken(authCode: string): Promise<AuthResponseDto> {
  return await fetch(`${dotenv.IDENTITY_SERVER_URL}/connect/token`, {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
      Accept: "application/json",
    },
    body: new URLSearchParams({
      client_id: dotenv.CLIENT_ID,
      client_secret: dotenv.CLIENT_SECRET,
      redirect_uri: `${dotenv.CLIENT_URL}/callback`,
      response_type: "code",
      grant_type: "authorization_code",
      code: authCode,
    }),
  }).then((res) => res.json());
}
