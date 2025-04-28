import { useGetProductsQuery } from "@/hooks/products/useGetProductsQuery";

export function getQueryKey(hookName: string) {
  switch (hookName) {
    case useGetProductsQuery.name:
      return ["products"];
    default:
      throw new Error(`Invalid function name: ${hookName}`);
  }
}
