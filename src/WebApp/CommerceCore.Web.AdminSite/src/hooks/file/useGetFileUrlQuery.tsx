import { getFileUrl } from "@/services/fileServices";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useQuery } from "@tanstack/react-query";

interface useGetFileUrlQueryProps {
  filePath: string;
}

export function useGetFileUrlQuery({ filePath }: useGetFileUrlQueryProps) {
  return useQuery({
    queryKey: getQueryKey("getFileUrl", { filePath }),
    queryFn: () => getFileUrl(filePath),
    enabled: !!filePath,
  });
}
