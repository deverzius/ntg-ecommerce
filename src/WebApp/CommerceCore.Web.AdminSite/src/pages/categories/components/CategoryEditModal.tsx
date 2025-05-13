import {Center, Modal} from "@mantine/core";
import {LoadingIndicator} from "@/shared/components/LoadingIndicator/LoadingIndicator";
import {CategoryEditForm} from "./CategoryEditForm";
import {useGetCategoryByIdQuery} from "@/hooks/category/useGetCategoryByIdQuery";
import {useGetCategoriesQuery} from "@/hooks/category/useGetCategoriesQuery";

interface CategoryEditModalProps {
    categoryId: string;
    opened: boolean;
    closeFn: () => void;
}

export function CategoryEditModal({
                                      categoryId,
                                      opened,
                                      closeFn,
                                  }: CategoryEditModalProps) {
    const {data: category} = useGetCategoryByIdQuery({
        id: categoryId,
    });
    const {data: categories} = useGetCategoriesQuery();

    return (
        <Modal opened={opened} onClose={close} title="Edit Category">
            {category ? (
                <CategoryEditForm
                    category={category}
                    categories={categories}
                    closeFn={closeFn}
                />
            ) : (
                <Center>
                    <LoadingIndicator/>
                </Center>
            )}
        </Modal>
    );
}
