import { dotenv } from "@/constants/dotenv";
import type { ProductDto } from "@/types/dto";

export async function getProducts(): Promise<ProductDto[]> {
  return await fetch(`${dotenv.API_URL}/v1/products`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export function getProductById() {}

export function createProduct() {}

export function updateProduct() {}

export function deleteProduct() {}
