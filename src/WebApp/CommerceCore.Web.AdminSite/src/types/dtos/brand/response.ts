import type { ProductResponseDto } from "../product/response";

export type BrandResponseDto = {
  id: string;
  name: string;
  description: string;
  products: ProductResponseDto[];
};
