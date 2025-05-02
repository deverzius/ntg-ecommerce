export type CategoryResponseDto = {
  id: string;
  name: string;
  description: string;
  parentCategoryId?: string;
  parentCategory?: CategoryResponseDto;
  products: CategoryResponseDto[];
};
