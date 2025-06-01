import { IconEdit } from "@/shared/icons/IconEdit";
import { IconTrash } from "@/shared/icons/IconTrash";
import {
  Button,
  Center,
  Flex,
  Group,
  Pagination,
  Paper,
  Stack,
  Table,
  Text,
  Title,
} from "@mantine/core";
import { CategoryEditModal } from "./CategoryEditModal";
import { useDisclosure } from "@mantine/hooks";
import { useState } from "react";
import { useSearchParams } from "react-router";
import { DEFAULT_PAGE_SIZE } from "@/shared/constants/common";
import { IconPlus } from "@/shared/icons/IconPlus";
import { CategoryCreateModal } from "./CategoryCreateModal";
import { CategoryDeleteModal } from "./CategoryDeleteModal";
import { categoryLabels } from "@/shared/constants/category";
import { useGetCategoriesQuery } from "@/hooks/category/useGetCategoriesQuery";

export function CategoryTable() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { data, refetch } = useGetCategoriesQuery(searchParams);

  const [selectedId, setSelectedId] = useState<string | undefined>();
  const [editModalOpened, { open: openEditModal, close: closeEditModal }] =
    useDisclosure(false);
  const [
    createModalOpened,
    { open: openCreateModal, close: closeCreateModal },
  ] = useDisclosure(false);
  const [
    deleteModalOpened,
    { open: openDeleteModal, close: closeDeleteModal },
  ] = useDisclosure(false);

  function handleEdit(id: string) {
    setSelectedId(id);
    openEditModal();
  }

  function handleDelete(id: string) {
    setSelectedId(id);
    openDeleteModal();
  }

  function handlePaginate(page: number) {
    searchParams.set("PageNumber", page.toString());
    searchParams.set("PageSize", DEFAULT_PAGE_SIZE.toString());
    setSearchParams(searchParams);
    refetch();
  }

  return (
    <Paper withBorder radius="lg" p="lg">
      <CategoryCreateModal
        opened={createModalOpened}
        closeFn={closeCreateModal}
      />

      {selectedId && (
        <CategoryEditModal
          categoryId={selectedId}
          opened={editModalOpened}
          closeFn={closeEditModal}
        />
      )}

      {selectedId && (
        <CategoryDeleteModal
          categoryId={selectedId}
          opened={deleteModalOpened}
          closeFn={closeDeleteModal}
        />
      )}

      <Stack>
        <Group justify="space-between">
          <Title>Categories</Title>
          <Button
            variant="outline"
            px="sm"
            leftSection={<IconPlus />}
            onClick={openCreateModal}
          >
            Add category
          </Button>
        </Group>

        <Paper withBorder radius="md">
          <Table>
            <Table.Thead>
              <Table.Tr>
                <Table.Th>{categoryLabels.name}</Table.Th>
                <Table.Th>{categoryLabels.description}</Table.Th>
                {/* <Table.Th>{categoryLabels.parentCategory}</Table.Th> */}
                <Table.Th>Actions</Table.Th>
              </Table.Tr>
            </Table.Thead>

            <Table.Tbody>
              {data?.items?.map((category) => (
                <Table.Tr key={category.id}>
                  <Table.Td>{category.name}</Table.Td>
                  <Table.Td>{category.description}</Table.Td>
                  {/* <Table.Td>{category.parentCategory?.name}</Table.Td> */}
                  <Table.Td>
                    <Flex gap="xs">
                      <Button
                        variant="outline"
                        px="sm"
                        onClick={() => handleEdit(category.id)}
                      >
                        <IconEdit size={20} />
                      </Button>
                      <Button
                        variant="outline"
                        color="red.7"
                        px="sm"
                        onClick={() => handleDelete(category.id)}
                      >
                        <IconTrash size={20} />
                      </Button>
                    </Flex>
                  </Table.Td>
                </Table.Tr>
              ))}
            </Table.Tbody>
          </Table>

          {data && (!data.items || data.items.length === 0) && (
            <Center my="md">
              <Text fz="md">No categories found</Text>
            </Center>
          )}
        </Paper>

        <Flex justify="flex-end">
          {data?.totalPages && (
            <Pagination
              total={data?.totalPages}
              withEdges
              onChange={handlePaginate}
            />
          )}
        </Flex>
      </Stack>
    </Paper>
  );
}
