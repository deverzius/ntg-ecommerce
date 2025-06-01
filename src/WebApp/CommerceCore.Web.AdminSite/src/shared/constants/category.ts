type CategoryLabelKey =
  | "id"
  | "name"
  | "description"
  | "createdDate"
  | "updatedDate"
  | "parentCategoryId"
  | "parentCategory";

export const categoryLabels: Record<CategoryLabelKey, string> = {
  id: "Id",
  name: "Name",
  description: "Description",
  createdDate: "Created Date",
  updatedDate: "Updated Date",
  parentCategoryId: "Parent Category Id",
  parentCategory: "Parent Category",
};
