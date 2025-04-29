import { useGetBrandsQuery } from "@/hooks/brand/useGetBrandsQuery";
import { useGetProductByIdQuery } from "@/hooks/product/useGetProductByIdQuery";
import { useGetProductsQuery } from "@/hooks/product/useGetProductsQuery";

export function getQueryKey(hookName: string, id?: string) {
  switch (hookName) {
    case useGetProductsQuery.name:
      return ["products"];
    case useGetProductByIdQuery.name:
      return ["product", id];
    case useGetBrandsQuery.name:
      return ["brands"];
    default:
      throw new Error(`Invalid function name: ${hookName}`);
  }
}
