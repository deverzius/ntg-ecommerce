import { getFileUrl, getPublicFileUrls } from "@/services/fileServices";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useQuery } from "@tanstack/react-query";

interface UseGetPublicFileUrlsQueryProps {
  limit: number;
  offset: number;
}

export function useGetPublicFileUrlsQuery({
  limit,
  offset,
}: UseGetPublicFileUrlsQueryProps) {
  return useQuery({
    queryKey: getQueryKey("getPublicFileUrls", { limit, offset }),
    queryFn: () => getPublicFileUrls(limit, offset),
  });
}
