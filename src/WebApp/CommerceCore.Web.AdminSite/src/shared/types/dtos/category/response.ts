export type CategoryResponseDto = {
  id: string;
  name: string;
  description: string;
  parentCategoryId?: string;
  parentCategory?: CategoryResponseDto;
  products: CategoryResponseDto[];
};

export type SimpleCategoryResponseDto = {
  id: string;
  name: string;
  description: string;
  parentCategoryId?: string;
};
