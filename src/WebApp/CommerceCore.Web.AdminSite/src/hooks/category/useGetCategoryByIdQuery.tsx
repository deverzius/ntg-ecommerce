import {getCategoryById} from "@/services/categoryServices";
import {getQueryKey} from "@/shared/utils/getQueryKey";
import {useQuery} from "@tanstack/react-query";

interface UseGetCategoryByIdQueryProps {
    id: string;
}

export function useGetCategoryByIdQuery({id}: UseGetCategoryByIdQueryProps) {
    return useQuery({
        queryKey: getQueryKey("getCategoryById", {id}),
        queryFn: () => getCategoryById(id),
    });
}
