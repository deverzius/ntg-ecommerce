import type { BrandResponseDto } from "../brand/response";

export type ProductResponseDto = {
  id: string;
  name: string;
  description: string;
  price: number;
  createdDate: string;
  updatedDate: string;
  brandId: string;

  brand?: BrandResponseDto;

  // TODO: update types
  images: any[];
  variants: any[];
  categories: any[];
  reviews: any[];
};
