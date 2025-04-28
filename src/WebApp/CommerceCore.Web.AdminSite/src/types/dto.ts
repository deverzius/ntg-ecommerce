export type ProductDto = {
  id: string;
  name: string;
  description: string;
  price: number;
  createdDate: string;
  updatedDate: string;
  brandId: string;

  // TODO: update types
  brand: any;
  images: any[];
  variants: any[];
  categories: any[];
  reviews: any[];
};
