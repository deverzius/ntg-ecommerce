import {deleteCategory} from "@/services/categoryServices";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    id: string;
};

export function useDeleteCategoryMutation() {
    return useMutation({
        mutationFn: ({id}: MutationFn) => deleteCategory(id),
    });
}
