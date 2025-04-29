import { getQueryKey } from "@/utils/getQueryKey";
import { getProductById } from "@/services/product-services";
import { useQueries, type UseQueryResult } from "@tanstack/react-query";
import { useGetProductByIdQuery } from "./useGetProductByIdQuery";
import { getBrands } from "@/services/brandServices";
import { useGetBrandsQuery } from "../brand/useGetBrandsQuery";

interface UseGetProductByIdQueryProps {
  id: string;
}

export function useGetProductForFormDataQuery({
  id,
}: UseGetProductByIdQueryProps) {
  return useQueries({
    queries: [
      {
        queryKey: getQueryKey(useGetProductByIdQuery.name, id),
        queryFn: () => getProductById(id),
      },
      {
        queryKey: getQueryKey(useGetBrandsQuery.name),
        queryFn: () => getBrands(),
      },
    ],
    combine: (result) => {
      return {
        product: result[0],
        brands: result[1],
      };
    },
  });
}
