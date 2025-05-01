import { getQueryKey } from "@/utils/getQueryKey";
import { useQuery } from "@tanstack/react-query";
import { getBrands } from "@/services/brandServices";

export function useGetBrandsQuery() {
  return useQuery({
    queryKey: getQueryKey("getBrands"),
    queryFn: getBrands,
  });
}
