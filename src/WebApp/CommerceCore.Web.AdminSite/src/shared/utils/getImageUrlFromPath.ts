import {dotenv} from "../constants/dotenv";

export function getImageUrlFromPath(path: string): string {
    return dotenv.SUPABASE_STORAGE_URL + "/object/public/ecommerce/" + path;
}
