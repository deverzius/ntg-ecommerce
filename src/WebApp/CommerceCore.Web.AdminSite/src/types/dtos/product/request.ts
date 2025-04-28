export type CreateProductRequestDto = {
  name: string;
  description: string;
  price: number;
  brandId: string;
};

export type UpdateProductRequestDto = {
  id: string;
  name: string;
  description: string;
  price: number;
  brandId: string;
};
