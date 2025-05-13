import {createProduct} from "@/services/productServices";
import type {CreateProductRequestDto} from "@/shared/types/dtos/product/request";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    productDto: CreateProductRequestDto;
};

export function useCreateProductMutation() {
    return useMutation({
        mutationFn: ({productDto}: MutationFn) => createProduct(productDto),
    });
}
