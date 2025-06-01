import { getQueryKey } from "@/shared/utils/getQueryKey";
import { getProductById } from "@/services/productServices";
import { useQueries } from "@tanstack/react-query";

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
    ],
    combine: (result) => {
      return {
        product: result[0],
      };
    },
  });
}
