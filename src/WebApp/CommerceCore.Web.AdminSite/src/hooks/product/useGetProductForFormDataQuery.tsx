import { getQueryKey } from "@/utils/getQueryKey";
import { getProductById } from "@/services/productServices";
import { useQueries } from "@tanstack/react-query";
import { getBrands } from "@/services/brandServices";

interface UseGetProductByIdQueryProps {
  id: string;
}

export function useGetProductForFormDataQuery({
  id,
}: UseGetProductByIdQueryProps) {
  return useQueries({
    queries: [
      {
        queryKey: getQueryKey("getProductById", { id }),
        queryFn: () => getProductById(id),
      },
      {
        queryKey: getQueryKey("getBrands"),
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
