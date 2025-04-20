export type ProductDto = {
  id: string;
  name: string;
  categoryId: string;
  description?: string;
  price: number;
  images: string[];
  createdDate: string;
  updatedDate?: string;
};
