import { dotenv } from "@/shared/constants/dotenv";
import type {
  CreateProductRequestDto,
  UpdateProductRequestDto,
} from "@/shared/types/dtos/product/request";
import type { ProductResponseDto } from "@/shared/types/dtos/product/response";
import type { PaginatedList } from "@/shared/types/PaginatedList";

export async function getProducts(
  urlParams?: URLSearchParams
): Promise<PaginatedList<ProductResponseDto>> {
  const stringParams = urlParams ? urlParams.toString() : "";

  return await fetch(`${dotenv.API_URL}/v1/products?${stringParams}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res);
}

export async function getProductById(id: string): Promise<ProductResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export async function createProduct(
  productDto: CreateProductRequestDto
): Promise<ProductResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/products`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify(productDto),
    credentials: "include",
  }).then((res) => res.json());
}

export async function updateProduct(
  id: string,
  productDto: UpdateProductRequestDto
): Promise<boolean> {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(productDto),
    credentials: "include",
  }).then((res) => res.ok);
}

export async function deleteProduct(id: string) {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "DELETE",
    credentials: "include",
  }).then((res) => res.ok);
}
