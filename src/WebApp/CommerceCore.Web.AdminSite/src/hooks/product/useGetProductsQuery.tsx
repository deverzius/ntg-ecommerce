import { getQueryKey } from "@/utils/getQueryKey";
import { getProducts } from "@/services/product-services";
import { useQuery } from "@tanstack/react-query";

export function useGetProductsQuery() {
  return useQuery({
    queryKey: getQueryKey(useGetProductsQuery.name),
    queryFn: getProducts,
  });
}
