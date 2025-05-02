import { Button, TextInput, Group, Select, NumberInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { productLabels } from "@/constants/product";
import { mapSelectOptions } from "@/shared/utils/mapSelectOptions";
import type { CreateProductRequestDto } from "@/types/dtos/product/request";
import type { BrandResponseDto } from "@/types/dtos/brand/response";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { useCreateProductMutation } from "@/hooks/product/useCreateProductMutation";
import { notifications } from "@mantine/notifications";

interface ProductCreateFormProps {
  brands: BrandResponseDto[];
  closeFn: () => void;
}

export function ProductCreateForm({ brands, closeFn }: ProductCreateFormProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useCreateProductMutation();

  const form = useForm<CreateProductRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      price: 0,
      description: "",
      brandId: "",
    },
    validate: {
      name: (value) => (value?.length > 0 ? null : "Name is required."),
      price: (value) => (value > 0 ? null : "Price must be greater than 0."),
      brandId: (value) => (value ? null : "Brand is required."),
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

      <Select
        label={productLabels.brand}
        data={mapSelectOptions(brands || [], "name", "id")}
        key={form.key("brandId")}
        {...form.getInputProps("brandId")}
        onChange={(value) => {
          value && form.setFieldValue("brandId", value);
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
