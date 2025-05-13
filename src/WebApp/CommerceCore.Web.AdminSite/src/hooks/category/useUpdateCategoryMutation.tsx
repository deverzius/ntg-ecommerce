import {updateCategory} from "@/services/categoryServices";
import type {UpdateCategoryRequestDto} from "@/shared/types/dtos/category/request";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    id: string;
    categoryDto: UpdateCategoryRequestDto;
};

export function useUpdateCategoryMutation() {
    return useMutation({
        mutationFn: ({id, categoryDto}: MutationFn) =>
            updateCategory(id, categoryDto),
    });
}
