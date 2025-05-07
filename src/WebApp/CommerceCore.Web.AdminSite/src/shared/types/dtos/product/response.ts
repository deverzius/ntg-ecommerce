import type { SimpleBrandResponseDto } from "../brand/response";
import type { SimpleCategoryResponseDto } from "../category/response";

export type ProductResponseDto = {
  id: string;
  name: string;
  description: string;
  price: number;
  createdDate: string;
  updatedDate: string;
  brandId: string;
  categoryId: string;

  brand?: SimpleBrandResponseDto;
  category?: SimpleCategoryResponseDto;
  images: ProductImageResponseDto[];
};

export type ProductImageResponseDto = {
  name: string;
  path: string;
  productId: string;
};

export type ReviewResponseDto = {
  id: string;
  rating: number;
  title: string;
  comment: string;
  createdDate: string;
  fullName?: string;
  phoneNumber?: string;
  email?: string;
};
