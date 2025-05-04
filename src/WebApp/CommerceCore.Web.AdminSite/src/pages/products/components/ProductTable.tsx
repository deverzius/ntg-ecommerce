import { useGetProductsQuery } from "@/hooks/product/useGetProductsQuery";
import { IconEdit } from "@/shared/icons/IconEdit";
import { IconTrash } from "@/shared/icons/IconTrash";
import {
  Text,
  Button,
  Center,
  Flex,
  Group,
  Pagination,
  Paper,
  Stack,
  Table,
  Title,
} from "@mantine/core";
import { ProductEditModal } from "./ProductEditModal";
import { useDisclosure } from "@mantine/hooks";
import { productLabels } from "@/shared/constants/product";
import { useState } from "react";
import { useSearchParams } from "react-router";
import { DEFAULT_PAGE_SIZE } from "@/shared/constants/common";
import { IconPlus } from "@/shared/icons/IconPlus";
import { ProductCreateModal } from "./ProductCreateModal";
import { ProductDeleteModal } from "./ProductDeleteModal";

export function ProductTable() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { data, refetch } = useGetProductsQuery(searchParams);

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
      <ProductCreateModal
        opened={createModalOpened}
        closeFn={closeCreateModal}
      />

      {selectedId && (
        <ProductEditModal
          productId={selectedId}
          opened={editModalOpened}
          closeFn={closeEditModal}
        />
      )}

      {selectedId && (
        <ProductDeleteModal
          productId={selectedId}
          opened={deleteModalOpened}
          closeFn={closeDeleteModal}
        />
      )}

      <Stack>
        <Group justify="space-between">
          <Title>Products</Title>
          <Button
            variant="outline"
            px="sm"
            leftSection={<IconPlus />}
            onClick={openCreateModal}
          >
            Add product
          </Button>
        </Group>

        <Paper withBorder radius="md">
          <Table>
            <Table.Thead>
              <Table.Tr>
                <Table.Th>{productLabels.id}</Table.Th>
                <Table.Th>{productLabels.name}</Table.Th>
                <Table.Th>{productLabels.price}</Table.Th>
                <Table.Th>{productLabels.updatedDate}</Table.Th>
                <Table.Th>{productLabels.brand}</Table.Th>
                <Table.Th>{productLabels.category}</Table.Th>
                <Table.Th>Actions</Table.Th>
              </Table.Tr>
            </Table.Thead>

            <Table.Tbody>
              {data?.items?.map((product) => (
                <Table.Tr key={product.id}>
                  <Table.Td>{product.id}</Table.Td>
                  <Table.Td>{product.name}</Table.Td>
                  <Table.Td>{product.price}</Table.Td>
                  <Table.Td>{product.updatedDate}</Table.Td>
                  <Table.Td>{product.brand?.name}</Table.Td>
                  <Table.Td>{product.category?.name}</Table.Td>
                  <Table.Td>
                    <Flex gap="xs">
                      <Button
                        variant="outline"
                        px="sm"
                        onClick={() => handleEdit(product.id)}
                      >
                        <IconEdit size={20} />
                      </Button>
                      <Button
                        variant="outline"
                        color="red.7"
                        px="sm"
                        onClick={() => handleDelete(product.id)}
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
              <Text fz="md">No products found</Text>
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
