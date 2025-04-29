import {
  Modal,
  Button,
  TextInput,
  NumberInput,
  Box,
  LoadingOverlay,
  Group,
  Select,
} from "@mantine/core";
import { useEffect, useState } from "react";
import { productLabels } from "@/constants/product";
import { formatDate } from "@/utils/formatDate";
import { useGetProductForFormDataQuery } from "@/hooks/product/useGetProductForFormDataQuery";
import { mapSelectOptions } from "@/utils/mapSelectOptions";

interface ProductEditModalProps {
  productId: string;
  opened: boolean;
  close: () => void;
}

export function ProductEditModal({
  productId,
  opened,
  close,
}: ProductEditModalProps) {
  const { product, brands } = useGetProductForFormDataQuery({
    id: productId,
  });
  const [showOverlay, setShowOverlay] = useState(true);

  useEffect(() => {
    product && setShowOverlay(false);
  }, [product.isLoading]);

  return (
    <Modal opened={opened} onClose={close} title="Edit Product">
      <Box pos="relative">
        <LoadingOverlay
          visible={showOverlay}
          zIndex={1000}
          overlayProps={{ blur: 1 }}
        />

        <TextInput
          label={productLabels.id}
          defaultValue={product.data?.id}
          disabled
        />

        <Group gap="xs">
          <TextInput
            label={productLabels.name}
            defaultValue={product.data?.name}
          />
          <NumberInput
            label={productLabels.price}
            defaultValue={product.data?.price}
          />
        </Group>

        <TextInput
          label={productLabels.description}
          defaultValue={product.data?.description}
        />

        <Group gap="xs">
          <TextInput
            label={productLabels.createdDate}
            defaultValue={formatDate(product.data?.createdDate)}
            disabled
          />
          <TextInput
            label={productLabels.updatedDate}
            defaultValue={formatDate(product.data?.updatedDate)}
            disabled
          />
        </Group>

        <TextInput label="Brand Id" defaultValue={product.data?.brandId} />

        {brands && product.data?.brandId && (
          <Select
            label={productLabels.brand}
            value={product.data?.brandId}
            data={mapSelectOptions(brands.data || [], "name", "id")}
          />
        )}

        <Group mt={24} gap="xs">
          <Button flex={1} onClick={close}>
            Save
          </Button>
          <Button flex={1} variant="outline" onClick={close}>
            Cancel
          </Button>
        </Group>
      </Box>
    </Modal>
  );
}
