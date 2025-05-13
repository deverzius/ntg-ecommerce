import {deleteProduct} from "@/services/productServices";
import {useMutation} from "@tanstack/react-query";

type MutationFn = {
    id: string;
};

export function useDeleteProductMutation() {
    return useMutation({
        mutationFn: ({id}: MutationFn) => deleteProduct(id),
    });
}
