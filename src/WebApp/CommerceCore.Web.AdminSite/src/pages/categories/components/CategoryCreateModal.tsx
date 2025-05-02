import { Modal } from "@mantine/core";
import { CategoryCreateForm } from "./CategoryCreateForm";
import { useGetCategoriesQuery } from "@/hooks/category/useGetCategoriesQuery";

interface CategoryCreateModalProps {
  opened: boolean;
  closeFn: () => void;
}

export function CategoryCreateModal({
  opened,
  closeFn,
}: CategoryCreateModalProps) {
  const { data } = useGetCategoriesQuery();

  return (
    <Modal opened={opened} onClose={closeFn} title="Create Category">
      <CategoryCreateForm categories={data?.items || []} closeFn={closeFn} />
    </Modal>
  );
}
