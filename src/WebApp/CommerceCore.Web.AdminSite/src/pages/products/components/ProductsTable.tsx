import { useGetProductsQuery } from "@/hooks/products/useGetProductsQuery";
import { FontWeight } from "@/types/enum";
import { Flex, Pagination, Paper, Stack, Table, Title } from "@mantine/core";
import { useEffect } from "react";

const headerColumns: Record<string, string> = {
  Id: "Id",
  Description: "Description",
  Price: "Price",
  CreatedDate: "Created Date",
  UpdatedDate: "Updated Date",
  BrandId: "Brand Id",
};

export function ProductsTable() {
  const { data } = useGetProductsQuery();

  return (
    <Paper withBorder radius="lg" p="lg">
      <Stack>
        <Title>Products</Title>

        <Paper withBorder radius="md">
          <Table>
            <Table.Thead>
              <Table.Tr>
                {Object.keys(headerColumns).map((headerColumnKey) => (
                  <Table.Th key={headerColumnKey} fw={FontWeight.Medium}>
                    {headerColumns[headerColumnKey]}
                  </Table.Th>
                ))}
                <Table.Th fw={FontWeight.Medium}>Actions</Table.Th>
              </Table.Tr>
            </Table.Thead>

            <Table.Tbody>
              {data?.map((product) => (
                <Table.Tr key={product.id}>
                  <Table.Td>{product.id}</Table.Td>
                  <Table.Td>{product.description}</Table.Td>
                  <Table.Td>{product.price}</Table.Td>
                  <Table.Td>{product.createdDate}</Table.Td>
                  <Table.Td>{product.updatedDate}</Table.Td>
                  <Table.Td>{product.brandId}</Table.Td>
                </Table.Tr>
              ))}
            </Table.Tbody>
          </Table>
        </Paper>

        <Flex justify="flex-end">
          <Pagination total={10} withEdges />
        </Flex>
      </Stack>
    </Paper>
  );
}
