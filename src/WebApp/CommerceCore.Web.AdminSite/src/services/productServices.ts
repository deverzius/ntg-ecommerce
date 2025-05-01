import { dotenv } from "@/constants/dotenv";
import type { ProductResponseDto } from "@/types/dtos/product/response";

export async function getProducts(): Promise<ProductResponseDto[]> {
  return await fetch(`${dotenv.API_URL}/v1/products`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export async function getProductById(id: string): Promise<ProductResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export function createProduct() {}

export function updateProduct() {}

export function deleteProduct() {}
