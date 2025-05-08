import { dotenv } from "@/shared/constants/dotenv";
import type {
  FileUrlResponseDto,
  PublicFileUrlResponseDto,
} from "@/shared/types/dtos/file/response";

export async function uploadFile(
  name: string,
  file: File
): Promise<FileUrlResponseDto> {
  const formData = new FormData();
  formData.append("name", name);
  formData.append("file", file);

  const response = await fetch(`${dotenv.API_URL}/v1/files`, {
    method: "POST",
    body: formData,
    headers: {
      Accept: "application/json",
    },
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Failed to upload file");
  }

  return await response.json();
}

export async function getFileUrl(
  filePath: string
): Promise<FileUrlResponseDto> {
  const encodedFilePath = encodeURIComponent(filePath);

  return await fetch(`${dotenv.API_URL}/v1/files/${encodedFilePath}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export async function getPublicFileUrls(
  limit: number,
  offset: number
): Promise<PublicFileUrlResponseDto[]> {
  return await fetch(
    `${dotenv.API_URL}/v1/files/public/list?limit=${limit}&offset=${offset}`,
    {
      method: "GET",
      headers: {
        Accept: "application/json",
      },
      credentials: "include",
    }
  ).then((res) => res.json());
}
