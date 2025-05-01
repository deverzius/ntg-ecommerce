import { dotenv } from "@/constants/dotenv";
import type { UpdateProductRequestDto } from "@/types/dtos/product/request";
import type { ProductResponseDto } from "@/types/dtos/product/response";

export async function getProducts(): Promise<ProductResponseDto[]> {
  return await fetch(`${dotenv.API_URL}/v1/products`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res.items);
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

export async function updateProduct(
  id: string,
  productDto: UpdateProductRequestDto
): Promise<boolean> {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify(productDto),
  }).then((res) => res.status == 204);
}

export function deleteProduct() {}
