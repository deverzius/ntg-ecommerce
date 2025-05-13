import {Button, Group, TextInput} from "@mantine/core";
import {useForm} from "@mantine/form";
import {useQueryClient} from "@tanstack/react-query";
import {getQueryKey} from "@/shared/utils/getQueryKey";
import {notifications} from "@mantine/notifications";
import type {CreateCategoryRequestDto} from "@/shared/types/dtos/category/request";
import {useCreateCategoryMutation} from "@/hooks/category/useCreateCategoryMutation";
import {categoryLabels} from "@/shared/constants/category";
import type {SimpleCategoryResponseDto} from "@/shared/types/dtos/category/response";

interface CategoryCreateFormProps {
    categories: SimpleCategoryResponseDto[];
    closeFn: () => void;
}

export function CategoryCreateForm({
                                       categories,
                                       closeFn,
                                   }: CategoryCreateFormProps) {
    const queryClient = useQueryClient();
    const {mutateAsync, isPending} = useCreateCategoryMutation();

    const form = useForm<CreateCategoryRequestDto>({
        mode: "uncontrolled",
        initialValues: {
            name: "",
            description: "",
        },
        validate: {
            name: (value) => (value.length > 0 ? null : "Name is required."),
        },
    });

    function handleSubmit(data: CreateCategoryRequestDto) {
        mutateAsync({
            categoryDto: data,
        })
            .then(() => {
                queryClient.invalidateQueries({
                    queryKey: getQueryKey("getCategories"),
                });
            })
            .then(() => {
                notifications.show({
                    color: "green",
                    title: "Success",
                    message: "Category created successfully.",
                });
                closeFn();
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

            {/* <Select
        label={categoryLabels.parentCategory}
        clearable
        data={mapSelectOptions(categories, "name", "id")}
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
