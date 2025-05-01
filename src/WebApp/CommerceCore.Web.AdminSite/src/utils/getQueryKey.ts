import { useGetBrandsQuery } from "@/hooks/brand/useGetBrandsQuery";
import { useGetProductByIdQuery } from "@/hooks/product/useGetProductByIdQuery";
import { useGetProductsQuery } from "@/hooks/product/useGetProductsQuery";

export function getQueryKey(hookName: string, data?: object) {
  const optionalData = data ? Object.values(data) : [];

  switch (hookName) {
    case useGetProductsQuery.name:
      return ["products", ...optionalData];
    case useGetProductByIdQuery.name:
      return ["product", ...optionalData];
    case useGetBrandsQuery.name:
      return ["brands", ...optionalData];
    default:
      throw new Error(`Invalid function name: ${hookName}`);
  }
}
