import { Button, Group, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useQueryClient } from "@tanstack/react-query";
import { getQueryKey } from "@/shared/utils/getQueryKey";
import { notifications } from "@mantine/notifications";
import type { CategoryResponse } from "@/shared/types/dtos/category/response";
import type { UpdateCategoryRequestDto } from "@/shared/types/dtos/category/request";
import { useUpdateCategoryMutation } from "@/hooks/category/useUpdateCategoryMutation";
import { categoryLabels } from "@/shared/constants/category";
import type { PagedResult } from "@/shared/types/PagedResult";
import { formatDatetime } from "@/shared/utils/formatDatetime";

interface CategoryEditFormProps {
  category: CategoryResponse;
  categories?: PagedResult<CategoryResponse>;
  closeFn: () => void;
}

export function CategoryEditForm({
  category,
  categories,
  closeFn,
}: CategoryEditFormProps) {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useUpdateCategoryMutation();

  const form = useForm<UpdateCategoryRequestDto>({
    mode: "uncontrolled",
    initialValues: {
      id: category.id,
      name: category.name,
      description: category.description,
      parentCategoryId: category.parentCategoryId,
    },
    validate: {
      name: (value) => (value.length > 0 ? null : "Name is required."),
    },
  });

  function handleSubmit(data: UpdateCategoryRequestDto) {
    mutateAsync({
      id: category.id,
      categoryDto: data,
    }).then((result) => {
      queryClient.invalidateQueries({
        queryKey: getQueryKey("getCategories"),
      });
      queryClient.invalidateQueries({
        queryKey: getQueryKey("getCategoryById", { id: category.id }),
      });

      if (result) {
        notifications.show({
          color: "green",
          title: "Success",
          message: "Category updated successfully.",
        });
        return;
      }
      notifications.show({
        color: "red",
        title: "Error",
        message: "Failed to update category.",
      });
    });
  }

  return (
    <form onSubmit={form.onSubmit((values) => handleSubmit(values))}>
      <TextInput
        withAsterisk
        label={categoryLabels.name}
        key={form.key("name")}
        {...form.getInputProps("name")}
      />

      <TextInput
        label={categoryLabels.description}
        key={form.key("description")}
        {...form.getInputProps("description")}
      />

      <TextInput
        label={categoryLabels.createdDate}
        value={formatDatetime(category.createdDate)}
        disabled
      />

      <TextInput
        label={categoryLabels.updatedDate}
        value={formatDatetime(category.updatedDate)}
        disabled
      />

      {/* <Select
        label={categoryLabels.parentCategory}
        clearable
        data={mapSelectOptions(
          categories?.items || [],
          "name",
          "id",
          (item) => item.id !== form.values.id // Avoid selecting the current category as parent category
        )}
        key={form.key("parentCategoryId")}
        {...form.getInputProps("parentCategoryId")}
        onChange={(value) => {
          value && form.setFieldValue("parentCategoryId", value);
        }}
      /> */}

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
