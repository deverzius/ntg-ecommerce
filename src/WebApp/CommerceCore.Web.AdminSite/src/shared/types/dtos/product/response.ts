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

  // TODO: update types
  variants: any[];
  reviews: any[];
};

export type ProductImageResponseDto = {
  name: string;
  path: string;
  productId: string;
};
