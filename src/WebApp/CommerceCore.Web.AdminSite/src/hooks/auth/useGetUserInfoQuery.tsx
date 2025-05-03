import { getUserInfo } from "@/services/authServices";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useQuery } from "@tanstack/react-query";

export function useGetUserInfoQuery() {
  return useQuery({
    queryKey: getQueryKey("getUserInfo"),
    queryFn: getUserInfo,
  });
}
