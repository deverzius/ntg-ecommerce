import {
  Button,
  TextInput,
  Group,
  Select,
  NumberInput,
  Alert,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { productLabels } from "@/constants/product";
import { formatDate } from "@/utils/formatDate";
import { mapSelectOptions } from "@/utils/mapSelectOptions";
import type { UpdateProductRequestDto } from "@/types/dtos/product/request";
import type { ProductResponseDto } from "@/types/dtos/product/response";
import type { BrandResponseDto } from "@/types/dtos/brand/response";
import { useUpdateProductMutation } from "@/hooks/product/useUpdateProductMutation";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/utils/getQueryKey";
import { useDisclosure } from "@mantine/hooks";

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

  const [alertOpened, { open: showAlert, close: hideAlert }] =
    useDisclosure(false);

  const form = useForm<UpdateProductRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      id: product.id,
      name: product.name,
      price: product.price,
      description: product.description,
      brandId: product.brandId,
    },
    onValuesChange: () => {
      hideAlert();
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
    })
      .then(() => {
        queryClient.invalidateQueries({
          queryKey: getQueryKey("getProducts"),
        });
        queryClient.invalidateQueries({
          queryKey: getQueryKey("getProductById", { id: product.id }),
        });
      })
      .then(() => {
        showAlert();
      });
  }

  return (
    <form onSubmit={form.onSubmit((values) => handleSubmit(values))}>
      <Alert
        variant="light"
        color="green"
        title="Submit Success"
        mb={12}
        hidden={!alertOpened}
      ></Alert>

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
