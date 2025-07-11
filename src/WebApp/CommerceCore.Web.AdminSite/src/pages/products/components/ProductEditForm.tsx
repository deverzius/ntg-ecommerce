import { Button, Group, NumberInput, Select, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { productLabels } from "@/shared/constants/product";
import { mapSelectOptions } from "@/shared/utils/mapSelectOptions";
import type { UpdateProductRequestDto } from "@/shared/types/dtos/product/request";
import type { ProductResponse } from "@/shared/types/dtos/product/response";
import { useUpdateProductMutation } from "@/hooks/product/useUpdateProductMutation";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { notifications } from "@mantine/notifications";
import type { CategoryResponse } from "@/shared/types/dtos/category/response";
import { useRef } from "react";
import ImagePicker from "@/shared/components/ImagePicker/ImagePicker";
import { formatDatetime } from "@/shared/utils/formatDatetime";

interface ProductEditFormProps {
  product: ProductResponse;
  categories: CategoryResponse[];
  closeFn: () => void;
}

export function ProductEditForm({
  product,
  categories,
  closeFn,
}: ProductEditFormProps) {
  const queryClient = useQueryClient();
  const { mutateAsync: updateProduct, isPending: isUpdateProductPending } =
    useUpdateProductMutation();
  const submitBtnRef = useRef<HTMLButtonElement>(null);

  const form = useForm<UpdateProductRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      id: product.id,
      name: product.name,
      price: product.price,
      description: product.description,
      categoryId: product.category.id,
      images: [],
    },
    validate: {
      name: (value) => (value.length > 0 ? null : "Name is required."),
      price: (value) => (value > 0 ? null : "Price must be greater than 0."),
      categoryId: (value) => (value ? null : "Category is required."),
    },
  });

  function handleSubmit(data: UpdateProductRequestDto) {
    updateProduct({
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
      <Group gap="xs" align="top">
        <TextInput
          flex={1}
          withAsterisk
          label={productLabels.name}
          key={form.key("name")}
          {...form.getInputProps("name")}
        />
        <TextInput
          label={productLabels.createdDate}
          value={formatDatetime(product.createdDate)}
          disabled
        />
        <TextInput
          label={productLabels.updatedDate}
          value={formatDatetime(product.updatedDate)}
          disabled
        />
      </Group>

      <TextInput
        label={productLabels.description}
        key={form.key("description")}
        {...form.getInputProps("description")}
      />

      <Group gap="xs" align="top">
        <NumberInput
          flex={1}
          withAsterisk
          label={productLabels.price}
          key={form.key("price")}
          {...form.getInputProps("price")}
        />
        <Select
          flex={1}
          withAsterisk
          label={productLabels.category}
          data={mapSelectOptions(categories || [], "name", "id")}
          key={form.key("categoryId")}
          {...form.getInputProps("categoryId")}
          onChange={(value) => {
            value && form.setFieldValue("categoryId", value);
          }}
        />
      </Group>

      <ImagePicker
        productId={product.id}
        handleSubmit={(selectedImages) =>
          form.setFieldValue("images", selectedImages)
        }
        defaultImages={product.variants}
      />

      <Group mt={24} gap="xs">
        <Button
          ref={submitBtnRef}
          loading={isUpdateProductPending}
          flex={1}
          type="submit"
        >
          Save
        </Button>
        <Button flex={1} variant="outline" onClick={closeFn}>
          Cancel
        </Button>
      </Group>
    </form>
  );
}
