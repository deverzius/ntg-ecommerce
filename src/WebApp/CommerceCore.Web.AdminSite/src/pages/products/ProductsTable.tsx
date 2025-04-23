import { DataTable } from "@/components/DataTable/DataTable";
import type { ProductDto } from "@/types/dto";

const mockProducts: ProductDto[] = [
  {
    id: "1",
    name: "Product 1",
    categoryId: "cat1",
    description: "Description for Product 1",
    price: 29.99,
    images: ["image1.jpg", "image2.jpg"],
    createdDate: "2023-01-01",
    updatedDate: "2023-01-02",
  },
  {
    id: "2",
    name: "Product 2",
    categoryId: "cat1",
    description: "Description for Product 2",
    price: 39.99,
    images: ["image3.jpg"],
    createdDate: "2023-02-01",
    updatedDate: "2023-02-02",
  },
  {
    id: "3",
    name: "Product 3",
    categoryId: "cat2",
    description: "Description for Product 3",
    price: 49.99,
    images: ["image4.jpg", "image5.jpg"],
    createdDate: "2023-03-01",
    updatedDate: "2023-03-02",
  },
  {
    id: "4",
    name: "Product 4",
    categoryId: "cat2",
    description: "Description for Product 4",
    price: 59.99,
    images: ["image6.jpg"],
    createdDate: "2023-04-01",
  },
  {
    id: "5",
    name: "Product 5",
    categoryId: "cat3",
    description: "Description for Product 5",
    price: 19.99,
    images: ["image7.jpg", "image8.jpg"],
    createdDate: "2023-05-01",
  },
  {
    id: "6",
    name: "Product 6",
    categoryId: "cat3",
    description: "Description for Product 6",
    price: 89.99,
    images: ["image9.jpg"],
    createdDate: "2023-06-01",
  },
  {
    id: "7",
    name: "Product 7",
    categoryId: "cat4",
    description: "Description for Product 7",
    price: 99.99,
    images: ["image10.jpg", "image11.jpg"],
    createdDate: "2023-07-01",
    updatedDate: "2023-07-02",
  },
  {
    id: "8",
    name: "Product 8",
    categoryId: "cat4",
    description: "Description for Product 8",
    price: 109.99,
    images: ["image12.jpg"],
    createdDate: "2023-08-01",
    updatedDate: "2023-08-02",
  },
];

export function ProductsTable() {
  return (
    <DataTable
      title="Products"
      data={mockProducts}
      columnTitles={[
        "Id",
        "Name",
        "Category Id",
        "Description",
        "Price",
        "Images",
        "Created Date",
        "Updated Date",
      ]}
    />
  );
}
