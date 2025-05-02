import { Button, TextInput, Group, Select, NumberInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { productLabels } from "@/shared/constants/product";
import { formatDate } from "@/shared/utils/formatDate";
import { mapSelectOptions } from "@/shared/utils/mapSelectOptions";
import type { UpdateProductRequestDto } from "@/shared/types/dtos/product/request";
import type { ProductResponseDto } from "@/shared/types/dtos/product/response";
import type { BrandResponseDto } from "@/shared/types/dtos/brand/response";
import { useUpdateProductMutation } from "@/hooks/product/useUpdateProductMutation";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { notifications } from "@mantine/notifications";

interface ProductEditFormProps {
  product: ProductResponseDto;
  brands: BrandResponseDto[];
  closeFn: () => void;
}

export function ProductEditForm({
  product,
  brands,
  closeFn,
}: ProductEditFormProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useUpdateProductMutation();

  const form = useForm<UpdateProductRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      id: product.id,
      name: product.name,
      price: product.price,
      description: product.description,
      brandId: product.brandId,
    },
    validate: {
      name: (value) => (value.length > 0 ? null : "Name is required."),
      price: (value) => (value > 0 ? null : "Price must be greater than 0."),
    },
  });

  function handleSubmit(data: UpdateProductRequestDto) {
    mutateAsync({
      id: product.id,
      productDto: data,
    }).then((result) => {
      queryClient.invalidateQueries({
        queryKey: getQueryKey("getProducts"),
      });
      queryClient.invalidateQueries({
        queryKey: getQueryKey("getProductById", { id: product.id }),
      });

      if (result) {
        notifications.show({
          color: "green",
          title: "Success",
          message: "Product updated successfully.",
        });
        return;
      }
      notifications.show({
        color: "red",
        title: "Error",
        message: "Failed to update product.",
      });
    });
  }

  return (
    <form onSubmit={form.onSubmit((values) => handleSubmit(values))}>
      <TextInput
        label={productLabels.id}
        value={form.getValues().id}
        disabled
      />

      <Group gap="xs" align="top">
        <TextInput
          label={productLabels.name}
          key={form.key("name")}
          {...form.getInputProps("name")}
        />
        <NumberInput
          label={productLabels.price}
          key={form.key("price")}
          {...form.getInputProps("price")}
        />
      </Group>

      <TextInput
        label={productLabels.description}
        key={form.key("description")}
        {...form.getInputProps("description")}
      />

      <Group gap="xs" align="top">
        <TextInput
          label={productLabels.createdDate}
          value={formatDate(product.createdDate)}
          disabled
        />
        <TextInput
          label={productLabels.updatedDate}
          value={formatDate(product.updatedDate)}
          disabled
        />
      </Group>

      {product.brandId && (
        <Select
          label={productLabels.brand}
          data={mapSelectOptions(brands || [], "name", "id")}
          key={form.key("brandId")}
          {...form.getInputProps("brandId")}
          onChange={(value) => {
            value && form.setFieldValue("brandId", value);
          }}
        />
      )}

      <Group mt={24} gap="xs">
        <Button loading={isPending} flex={1} type="submit">
          Save
        </Button>
        <Button flex={1} variant="outline" onClick={closeFn}>
          Cancel
        </Button>
      </Group>
    </form>
  );
}
