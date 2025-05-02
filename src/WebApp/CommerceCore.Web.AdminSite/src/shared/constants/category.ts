type CategoryLabelKey =
  | "id"
  | "name"
  | "description"
  | "parentCategoryId"
  | "parentCategory";

export const categoryLabels: Record<CategoryLabelKey, string> = {
  id: "Id",
  name: "Name",
  description: "Description",
  parentCategoryId: "Parent Category Id",
  parentCategory: "Parent Category",
};
