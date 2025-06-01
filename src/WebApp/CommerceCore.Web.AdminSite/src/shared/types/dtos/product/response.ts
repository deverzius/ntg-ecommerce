import type { CategoryResponse } from "../category/response";

export type ProductResponse = {
  id: string;
  name: string;
  description: string;
  price: number;
  createdDate: string;
  updatedDate: string;

  category: CategoryResponse;
  // TODO: update types
  variants: any;
};

export type ProductImageResponseDto = {
  name: string;
  publicUrl: string;
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
