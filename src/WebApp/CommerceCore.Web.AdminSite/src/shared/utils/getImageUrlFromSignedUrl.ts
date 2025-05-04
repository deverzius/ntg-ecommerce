import { dotenv } from "../constants/dotenv";

export function getImageUrlFromSignedUrl(signedUrl: string): string {
  return dotenv.SUPABASE_STORAGE_URL + signedUrl;
}
