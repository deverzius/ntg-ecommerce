import type { Route } from "../+types/root";
import { ProductsPage } from "@/pages/products";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function Page() {
  return <ProductsPage />;
}
