export type CreateProductRequestDto = {
  name: string;
  description: string;
  price: number;
  categoryId: string;
};

export type UpdateProductRequestDto = {
  id: string;
  name: string;
  description: string;
  price: number;
  categoryId: string;
  images: ProductImageRequestDto[];
};

export type ProductImageRequestDto = {
  name: string;
  path: string;
};
