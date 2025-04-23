import { Flex, Pagination, Paper, Stack, Table, Title } from "@mantine/core";
import { FontWeight } from "@/types/enum";

type DataTableRow = { id: string } & object;

interface DataTableProps {
  title: string;
  data: DataTableRow[];
  columnTitles: string[];
}

export function DataTable({ title, data, columnTitles }: DataTableProps) {
  return (
    <Paper
      bg="white"
      withBorder
      radius="lg"
      styles={{ root: { overflow: "hidden" } }}
      p="lg"
    >
      <Stack>
        <Title size="h3">{title}</Title>

        <Paper
          bg="white"
          withBorder
          radius="md"
          styles={{ root: { overflow: "hidden" } }}
        >
          <Table
            verticalSpacing="sm"
            horizontalSpacing="xl"
            borderColor="gray.1"
          >
            <DataTableColumnTitles columnTitles={columnTitles} />
            <DataTableBody data={data} />
          </Table>
        </Paper>

        <Flex justify="flex-end">
          <Pagination total={10} withEdges />
        </Flex>
      </Stack>
    </Paper>
  );
}

interface DataTableColumnTitlesProps {
  columnTitles: DataTableProps["columnTitles"];
}

function DataTableColumnTitles({ columnTitles }: DataTableColumnTitlesProps) {
  return (
    <Table.Thead bg="gray.0">
      <Table.Tr c="blue.6">
        {columnTitles.map((ct) => (
          <Table.Th key={ct} fz="md" fw={FontWeight.Medium}>
            {ct}
          </Table.Th>
        ))}
      </Table.Tr>
    </Table.Thead>
  );
}

interface DataTableBodyProps {
  data: DataTableProps["data"];
}

function DataTableBody({ data }: DataTableBodyProps) {
  return (
    <Table.Tbody>
      {data.map((rowData) => (
        <Table.Tr c="gray.9">
          {Object.values(rowData).map((value, idx) => (
            <Table.Td key={idx} fz="md">
              {value}
            </Table.Td>
          ))}
        </Table.Tr>
      ))}
    </Table.Tbody>
  );
}
