type ProductLabelKey =
  | "id"
  | "name"
  | "description"
  | "price"
  | "createdDate"
  | "updatedDate"
  | "category"
  | "files";

export const productLabels: Record<ProductLabelKey, string> = {
  id: "Id",
  name: "Name",
  description: "Description",
  price: "Price",
  createdDate: "Created Date",
  updatedDate: "Updated Date",
  category: "Category",
  files: "Files",
};
