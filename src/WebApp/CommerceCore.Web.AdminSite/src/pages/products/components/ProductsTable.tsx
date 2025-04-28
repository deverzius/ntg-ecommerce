import { FontWeight } from "@/types/enum";
import { Flex, Pagination, Paper, Stack, Table, Title } from "@mantine/core";

const headerColumns: Record<string, string> = {
  Id: "Id",
  Description: "Description",
  Price: "Price",
  CreatedDate: "Created Date",
  UpdatedDate: "Updated Date",
  BrandId: "Brand Id",
};

export function ProductsTable() {
  return (
    <Paper withBorder radius="lg" p="lg">
      <Stack>
        <Title>Products</Title>

        <Paper withBorder radius="md">
          <Table>
            <Table.Thead>
              {Object.keys(headerColumns).map((headerColumnKey) => (
                <Table.Th key={headerColumnKey} fw={FontWeight.Medium}>
                  {headerColumns[headerColumnKey]}
                </Table.Th>
              ))}
              <Table.Th fw={FontWeight.Medium}>Actions</Table.Th>
            </Table.Thead>

            <Table.Tbody>
              <Table.Tr>
                <Table.Td>table cell</Table.Td>
              </Table.Tr>
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
