import {createCategory} from "@/services/categoryServices";
import type {CreateCategoryRequestDto} from "@/shared/types/dtos/category/request";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    categoryDto: CreateCategoryRequestDto;
};

export function useCreateCategoryMutation() {
    return useMutation({
        mutationFn: ({categoryDto}: MutationFn) => createCategory(categoryDto),
    });
}
