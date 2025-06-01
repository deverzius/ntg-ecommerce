import { Button, Group, NumberInput, Select, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { productLabels } from "@/shared/constants/product";
import { mapSelectOptions } from "@/shared/utils/mapSelectOptions";
import type { CreateProductRequestDto } from "@/shared/types/dtos/product/request";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useCreateProductMutation } from "@/hooks/product/useCreateProductMutation";
import { notifications } from "@mantine/notifications";
import type { CategoryResponse } from "@/shared/types/dtos/category/response";

interface ProductCreateFormProps {
  brands: any[];
  categories: CategoryResponse[];
  closeFn: () => void;
}

export function ProductCreateForm({
  brands,
  categories,
  closeFn,
}: ProductCreateFormProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useCreateProductMutation();

  const form = useForm<CreateProductRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      price: 0,
      description: "",
      brandId: "",
      categoryId: "",
    },
    validate: {
      name: (value) => (value.length > 0 ? null : "Name is required."),
      price: (value) => (value > 0 ? null : "Price must be greater than 0."),
      brandId: (value) => (value ? null : "Brand is required."),
      categoryId: (value) => (value ? null : "Category is required."),
    },
  });

  function handleSubmit(data: CreateProductRequestDto) {
    mutateAsync({
      productDto: data,
    })
      .then(() => {
        queryClient.invalidateQueries({
          queryKey: getQueryKey("getProducts"),
        });
      })
      .then(() => {
        notifications.show({
          color: "green",
          title: "Success",
          message: "Product created successfully.",
        });
        closeFn();
      });
  }

  return (
    <form onSubmit={form.onSubmit((values) => handleSubmit(values))}>
      <Group gap="xs" align="top">
        <TextInput
          withAsterisk
          label={productLabels.name}
          key={form.key("name")}
          {...form.getInputProps("name")}
        />
        <NumberInput
          withAsterisk
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

      <Select
        withAsterisk
        allowDeselect={false}
        label={productLabels.brand}
        data={mapSelectOptions(brands || [], "name", "id")}
        key={form.key("brandId")}
        {...form.getInputProps("brandId")}
        onChange={(value) => {
          value && form.setFieldValue("brandId", value);
        }}
      />

      <Select
        withAsterisk
        allowDeselect={false}
        label={productLabels.category}
        data={mapSelectOptions(categories || [], "name", "id")}
        key={form.key("categoryId")}
        {...form.getInputProps("categoryId")}
        onChange={(value) => {
          value && form.setFieldValue("categoryId", value);
        }}
      />

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
