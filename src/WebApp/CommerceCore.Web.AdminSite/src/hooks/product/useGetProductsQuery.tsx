import { getQueryKey } from "@/shared/utils/getQueryKey";
import { getProducts } from "@/services/productServices";
import { useQuery } from "@tanstack/react-query";

export function useGetProductsQuery(urlParams?: URLSearchParams) {
  return useQuery({
    queryKey: getQueryKey("getProducts", urlParams),
    queryFn: () => getProducts(urlParams),
  });
}
