import {updateProduct} from "@/services/productServices";
import type {UpdateProductRequestDto} from "@/shared/types/dtos/product/request";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    id: string;
    productDto: UpdateProductRequestDto;
};

export function useUpdateProductMutation() {
    return useMutation({
        mutationFn: ({id, productDto}: MutationFn) =>
            updateProduct(id, productDto),
    });
}
