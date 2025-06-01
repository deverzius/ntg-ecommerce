export type CategoryResponse = {
  id: string;
  name: string;
  description: string;
  parentCategoryId?: string;
  createdDate: Date;
  updatedDate: Date;
};
