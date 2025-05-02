import { dotenv } from "@/shared/constants/dotenv";
import type { BrandResponseDto } from "@/shared/types/dtos/brand/response";
import type { PaginatedList } from "@/shared/types/PaginatedList";

export async function getBrands(): Promise<PaginatedList<BrandResponseDto>> {
  return await fetch(`${dotenv.API_URL}/v1/brands`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res);
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
