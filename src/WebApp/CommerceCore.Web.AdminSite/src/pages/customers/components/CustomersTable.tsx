import { FontWeight } from "@/shared/types/enum";
import { Flex, Pagination, Paper, Stack, Table, Title } from "@mantine/core";

const data = [
  {
    title: "Foundation",
    author: "Isaac Asimov",
    year: 1951,
    reviews: { positive: 2223, negative: 259 },
  },
  {
    title: "Frankenstein",
    author: "Mary Shelley",
    year: 1818,
    reviews: { positive: 5677, negative: 1265 },
  },
  {
    title: "Solaris",
    author: "Stanislaw Lem",
    year: 1961,
    reviews: { positive: 3487, negative: 1845 },
  },
  {
    title: "Dune",
    author: "Frank Herbert",
    year: 1965,
    reviews: { positive: 8576, negative: 663 },
  },
  {
    title: "The Left Hand of Darkness",
    author: "Ursula K. Le Guin",
    year: 1969,
    reviews: { positive: 6631, negative: 993 },
  },
  {
    title: "A Scanner Darkly",
    author: "Philip K Dick",
    year: 1977,
    reviews: { positive: 8124, negative: 1847 },
  },
];

export function CustomersTable() {
  const rows = data.map((row) => {
    const totalReviews = row.reviews.negative + row.reviews.positive;

    return (
      <Table.Tr key={row.title} c="gray.9">
        <Table.Td fz="md">{row.title}</Table.Td>
        <Table.Td fz="md">{row.author}</Table.Td>
        <Table.Td fz="md">{row.year}</Table.Td>
        <Table.Td fz="md">{Intl.NumberFormat().format(totalReviews)}</Table.Td>
      </Table.Tr>
    );
  });

  return (
    <Paper
      bg="white"
      withBorder
      radius="lg"
      styles={{ root: { overflow: "hidden" } }}
      p="lg"
    >
      <Stack>
        <Title size="h3">Customers</Title>

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
            <Table.Thead bg="gray.0">
              <Table.Tr c="blue.6">
                <Table.Th fz="md" fw={FontWeight.Medium}>
                  ID
                </Table.Th>
                <Table.Th fz="md" fw={FontWeight.Medium}>
                  User Name
                </Table.Th>
                <Table.Th fz="md" fw={FontWeight.Medium}>
                  Email
                </Table.Th>
                <Table.Th fz="md" fw={FontWeight.Medium}>
                  Phone Number
                </Table.Th>
              </Table.Tr>
            </Table.Thead>
            <Table.Tbody>{rows}</Table.Tbody>
          </Table>
        </Paper>

        <Flex justify="flex-end">
          <Pagination total={10} withEdges />
        </Flex>
      </Stack>
    </Paper>
  );
}
