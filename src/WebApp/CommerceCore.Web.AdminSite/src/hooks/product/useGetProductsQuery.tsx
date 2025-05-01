import { getQueryKey } from "@/utils/getQueryKey";
import { getProducts } from "@/services/productServices";
import { useQuery } from "@tanstack/react-query";

export function useGetProductsQuery(urlParams?: URLSearchParams) {
  return useQuery({
    queryKey: getQueryKey(useGetProductsQuery.name, urlParams),
    queryFn: () => getProducts(urlParams),
  });
}
