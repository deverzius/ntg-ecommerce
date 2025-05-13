import {getQueryKey} from "@/shared/utils/getQueryKey";
import {useQuery} from "@tanstack/react-query";
import {getCategories} from "@/services/categoryServices";

export function useGetCategoriesQuery(urlParams?: URLSearchParams) {
    return useQuery({
        queryKey: getQueryKey("getCategories", urlParams),
        queryFn: () => getCategories(urlParams),
    });
}
