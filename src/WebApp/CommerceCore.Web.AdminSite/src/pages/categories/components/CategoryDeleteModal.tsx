import { Button, Group, Modal, Text } from "@mantine/core";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useQueryClient } from "@tanstack/react-query";
import { notifications } from "@mantine/notifications";
import { useDeleteCategoryMutation } from "@/hooks/category/useDeleteCategoryMutation";

interface CategoryDeleteModalProps {
  categoryId: string;
  opened: boolean;
  closeFn: () => void;
}

export function CategoryDeleteModal({
  categoryId,
  opened,
  closeFn,
}: CategoryDeleteModalProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useDeleteCategoryMutation();

  function handleDelete() {
    mutateAsync({ id: categoryId }).then((result) => {
      if (result) {
        notifications.show({
          color: "green",
          title: "Success",
          message: "Category deleted successfully.",
        });
        queryClient.invalidateQueries({
          queryKey: getQueryKey("getCategories"),
        });
        closeFn();
        return;
      }
      notifications.show({
        color: "red",
        title: "Error",
        message: "Failed to delete category.",
      });
      closeFn();
    });
  }

  return (
    <Modal opened={opened} onClose={closeFn} title="Delete Category">
      <Text fz="lg">Do you want to delete this category?</Text>
      <Group mt={24} gap="xs">
        <Button loading={isPending} flex={1} color="red" onClick={handleDelete}>
          Delete
        </Button>
        <Button flex={1} variant="outline" color="gray" onClick={closeFn}>
          Cancel
        </Button>
      </Group>
    </Modal>
  );
}
