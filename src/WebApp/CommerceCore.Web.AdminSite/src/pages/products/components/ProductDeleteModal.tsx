import { Button, Group, Modal, Text } from "@mantine/core";
import { useDeleteProductMutation } from "@/hooks/product/useDeleteProductMutation";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useQueryClient } from "@tanstack/react-query";
import { notifications } from "@mantine/notifications";

interface ProductDeleteModalProps {
  productId: string;
  opened: boolean;
  closeFn: () => void;
}

export function ProductDeleteModal({
  productId,
  opened,
  closeFn,
}: ProductDeleteModalProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useDeleteProductMutation();

  function handleDelete() {
    mutateAsync({ id: productId }).then((result) => {
      if (result) {
        notifications.show({
          color: "green",
          title: "Success",
          message: "Product deleted successfully.",
        });
        queryClient.invalidateQueries({ queryKey: getQueryKey("getProducts") });
        closeFn();
        return;
      }
      notifications.show({
        color: "red",
        title: "Error",
        message: "Failed to delete product.",
      });
      closeFn();
    });
  }

  return (
    <Modal opened={opened} onClose={closeFn} title="Delete Product">
      <Text fz="lg">Do you want to delete this product?</Text>
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
