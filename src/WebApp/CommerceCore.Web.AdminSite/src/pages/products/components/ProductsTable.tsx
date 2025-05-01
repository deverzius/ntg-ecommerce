import { useGetProductsQuery } from "@/hooks/product/useGetProductsQuery";
import { IconEdit } from "@/shared/icons/IconEdit";
import { IconTrash } from "@/shared/icons/IconTrash";
import { FontWeight } from "@/types/enum";
import {
  Button,
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
import { productLabels } from "@/constants/product";
import { useState } from "react";
import { useSearchParams } from "react-router";
import { DEFAULT_PAGE_SIZE } from "@/constants/common";
import { IconPlus } from "@/shared/icons/IconPlus";
import { ProductCreateModal } from "./ProductCreateModal";

export function ProductsTable() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { data, refetch } = useGetProductsQuery(searchParams);
  const [selectedId, setSelectedId] = useState<string | undefined>();
  const [editModalOpened, { open: openEditModal, close: closeEditModal }] =
    useDisclosure(false);
  const [
    createModalOpened,
    { open: openCreateModal, close: closeCreateModal },
  ] = useDisclosure(false);

  function handleEdit(id: string) {
    setSelectedId(id);
    openEditModal();
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
                <Table.Td>{productLabels.id}</Table.Td>
                <Table.Td>{productLabels.name}</Table.Td>
                <Table.Td>{productLabels.description}</Table.Td>
                <Table.Td>{productLabels.price}</Table.Td>
                <Table.Td>{productLabels.createdDate}</Table.Td>
                <Table.Td>{productLabels.updatedDate}</Table.Td>
                <Table.Td>{productLabels.brand}</Table.Td>
                <Table.Th fw={FontWeight.Medium}>Actions</Table.Th>
              </Table.Tr>
            </Table.Thead>

            <Table.Tbody>
              {data?.items?.map((product) => (
                <Table.Tr key={product.id}>
                  <Table.Td>{product.id}</Table.Td>
                  <Table.Td>{product.name}</Table.Td>
                  <Table.Td>{product.description}</Table.Td>
                  <Table.Td>{product.price}</Table.Td>
                  <Table.Td>{product.createdDate}</Table.Td>
                  <Table.Td>{product.updatedDate}</Table.Td>
                  <Table.Td>{product.brand?.name}</Table.Td>
                  <Table.Td>
                    <Flex gap="xs">
                      <Button
                        variant="outline"
                        px="sm"
                        onClick={() => handleEdit(product.id)}
                      >
                        <IconEdit size={20} />
                      </Button>
                      <Button variant="outline" color="red.7" px="sm">
                        <IconTrash size={20} />
                      </Button>
                    </Flex>
                  </Table.Td>
                </Table.Tr>
              ))}
            </Table.Tbody>
          </Table>
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
