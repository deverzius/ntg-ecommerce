import { dotenv } from "@/constants/dotenv";
import type { BrandResponseDto } from "@/types/dtos/brand/response";

export async function getBrands(): Promise<BrandResponseDto[]> {
  return await fetch(`${dotenv.API_URL}/v1/brands`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res.items);
}

export async function getBrandById(id: string): Promise<BrandResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/brands/${id}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export function createBrand() {}

export function updateBrand() {}

export function deleteBrand() {}
