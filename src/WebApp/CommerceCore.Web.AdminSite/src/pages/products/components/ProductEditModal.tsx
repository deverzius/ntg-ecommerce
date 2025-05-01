import { Modal, Center } from "@mantine/core";
import { useGetProductForFormDataQuery } from "@/hooks/product/useGetProductForFormDataQuery";
import { LoadingIndicator } from "@/components/LoadingIndicator/LoadingIndicator";
import { ProductEditForm } from "./ProductEditForm";

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

  return (
    <Modal opened={opened} onClose={close} title="Edit Product">
      {product.data ? (
        <ProductEditForm
          product={product.data}
          brands={brands.data || []}
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
