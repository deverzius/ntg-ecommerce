type ProductLabelKey =
  | "id"
  | "name"
  | "description"
  | "price"
  | "createdDate"
  | "updatedDate"
  | "brandId"
  | "brand"
  | "category";

export const productLabels: Record<ProductLabelKey, string> = {
  id: "Id",
  name: "Name",
  description: "Description",
  price: "Price",
  createdDate: "Created Date",
  updatedDate: "Updated Date",
  brandId: "Brand Id",
  brand: "Brand",
  category: "Category",
};
