import type { ProductResponseDto } from "../product/response";

export type BrandResponseDto = {
  id: string;
  name: string;
  description: string;
  products: ProductResponseDto[];
};

export type SimpleBrandResponseDto = {
  id: string;
  name: string;
  description: string;
};
