import { Modal } from "@mantine/core";
import { ProductCreateForm } from "./ProductCreateForm";
import { useGetBrandsQuery } from "@/hooks/brand/useGetBrandsQuery";

interface ProductCreateModalProps {
  opened: boolean;
  closeFn: () => void;
}

export function ProductCreateModal({
  opened,
  closeFn,
}: ProductCreateModalProps) {
  const { data } = useGetBrandsQuery();

  return (
    <Modal opened={opened} onClose={closeFn} title="Create Product">
      <ProductCreateForm brands={data?.items || []} closeFn={closeFn} />
    </Modal>
  );
}
