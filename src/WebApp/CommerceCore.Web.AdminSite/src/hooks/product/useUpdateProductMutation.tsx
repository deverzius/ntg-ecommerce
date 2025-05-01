import { updateProduct } from "@/services/productServices";
import type { UpdateProductRequestDto } from "@/types/dtos/product/request";
import { useMutation } from "@tanstack/react-query";

type mutationFnType = {
  id: string;
  productDto: UpdateProductRequestDto;
};

export function useUpdateProductMutation() {
  return useMutation({
    mutationFn: ({ id, productDto }: mutationFnType) =>
      updateProduct(id, productDto),
  });
}
