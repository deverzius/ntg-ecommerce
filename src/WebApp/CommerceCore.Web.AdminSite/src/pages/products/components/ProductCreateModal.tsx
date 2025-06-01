import { Modal } from "@mantine/core";
import { ProductCreateForm } from "./ProductCreateForm";
import { useGetCategoriesQuery } from "@/hooks/category/useGetCategoriesQuery";

interface ProductCreateModalProps {
  opened: boolean;
  closeFn: () => void;
}

export function ProductCreateModal({
  opened,
  closeFn,
}: ProductCreateModalProps) {
  const { data: categories } = useGetCategoriesQuery();

  return (
    <Modal opened={opened} onClose={closeFn} title="Create Product">
      <ProductCreateForm
        categories={categories?.items || []}
        closeFn={closeFn}
      />
    </Modal>
  );
}
