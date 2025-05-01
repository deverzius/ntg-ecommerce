import { getQueryKey } from "@/utils/getQueryKey";
import { getProductById } from "@/services/productServices";
import { useQuery } from "@tanstack/react-query";

interface UseGetProductByIdQueryProps {
  id: string;
}

export function useGetProductByIdQuery({ id }: UseGetProductByIdQueryProps) {
  return useQuery({
    queryKey: getQueryKey(useGetProductByIdQuery.name, { id }),
    queryFn: () => getProductById(id),
  });
}
