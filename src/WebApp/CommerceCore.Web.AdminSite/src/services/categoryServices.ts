import { dotenv } from "@/shared/constants/dotenv";
import type {
  CreateCategoryRequestDto,
  UpdateCategoryRequestDto,
} from "@/shared/types/dtos/category/request";
import type { CategoryResponseDto } from "@/shared/types/dtos/category/response";
import type { PaginatedList } from "@/shared/types/PaginatedList";

export async function getCategories(
  urlParams?: URLSearchParams
): Promise<PaginatedList<CategoryResponseDto>> {
  const stringParams = urlParams ? urlParams.toString() : "";

  return await fetch(`${dotenv.API_URL}/v1/categories?${stringParams}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((res) => res.json())
    .then((res) => res);
}

export async function getCategoryById(
  id: string
): Promise<CategoryResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/categories/${id}`, {
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  }).then((res) => res.json());
}

export async function createCategory(
  categoryDto: CreateCategoryRequestDto
): Promise<CategoryResponseDto> {
  return await fetch(`${dotenv.API_URL}/v1/categories`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify(categoryDto),
  }).then((res) => res.json());
}

export async function updateCategory(
  id: string,
  categoryDto: UpdateCategoryRequestDto
): Promise<boolean> {
  return await fetch(`${dotenv.API_URL}/v1/categories/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(categoryDto),
  }).then((res) => res.status === 204);
}

export async function deleteCategory(id: string) {
  return await fetch(`${dotenv.API_URL}/v1/categories/${id}`, {
    method: "DELETE",
  }).then((res) => res.status === 204);
}
