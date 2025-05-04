import { Modal, Center } from "@mantine/core";
import { useGetProductForFormDataQuery } from "@/hooks/product/useGetProductForFormDataQuery";
import { LoadingIndicator } from "@/shared/components/LoadingIndicator/LoadingIndicator";
import { ProductEditForm } from "./ProductEditForm";
import { useGetCategoriesQuery } from "@/hooks/category/useGetCategoriesQuery";

interface ProductEditModalProps {
  productId: string;
  opened: boolean;
  closeFn: () => void;
}

export function ProductEditModal({
  productId,
  opened,
  closeFn,
}: ProductEditModalProps) {
  const { product, brands } = useGetProductForFormDataQuery({
    id: productId,
  });
  const { data: categories } = useGetCategoriesQuery();

  return (
    <Modal opened={opened} onClose={close} title="Edit Product" size="xl">
      {product.data ? (
        <ProductEditForm
          product={product.data}
          categories={categories?.items || []}
          brands={brands.data?.items || []}
          closeFn={closeFn}
        />
      ) : (
        <Center>
          <LoadingIndicator />
        </Center>
      )}
    </Modal>
  );
}
