import { dotenv } from "@/shared/constants/dotenv";
import type {
  CreateProductRequestDto,
  UpdateProductRequestDto,
} from "@/shared/types/dtos/product/request";
import type { ProductResponse } from "@/shared/types/dtos/product/response";
import type { PagedResult } from "@/shared/types/PagedResult";

export async function getProducts(
  urlParams?: URLSearchParams
): Promise<PagedResult<ProductResponse>> {
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

export async function getProductById(id: string): Promise<ProductResponse> {
  return await fetch(`${dotenv.API_URL}/v1/products/${id}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res);
}

export async function createProduct(
  productDto: CreateProductRequestDto
): Promise<ProductResponse> {
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
