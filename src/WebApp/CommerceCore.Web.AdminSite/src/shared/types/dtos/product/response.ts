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

  // TODO: update types
  images: any[];
  variants: any[];
  reviews: any[];
};
