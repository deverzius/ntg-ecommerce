import { Text, Center, Group, Paper, Stack, Table, Title } from "@mantine/core";
import { useGetAllCustomersQuery } from "@/hooks/customer/useGetAllCustomers";
import { customerLabels } from "@/shared/constants/customer";

export function CustomerTable() {
  const { data } = useGetAllCustomersQuery();

  // function handlePaginate(page: number) {
  //   searchParams.set("PageNumber", page.toString());
  //   searchParams.set("PageSize", DEFAULT_PAGE_SIZE.toString());
  //   setSearchParams(searchParams);
  //   refetch();
  // }

  return (
    <Paper withBorder radius="lg" p="lg">
      <Stack>
        <Group justify="space-between">
          <Title>Customers</Title>
        </Group>

        <Paper withBorder radius="md">
          <Table>
            <Table.Thead>
              <Table.Tr>
                <Table.Th>{customerLabels.id}</Table.Th>
                <Table.Th>{customerLabels.userName}</Table.Th>
                <Table.Th>{customerLabels.email}</Table.Th>
                <Table.Th>{customerLabels.phoneNumber}</Table.Th>
              </Table.Tr>
            </Table.Thead>

            <Table.Tbody>
              {data?.map((customer) => (
                <Table.Tr key={customer.id}>
                  <Table.Td>{customer.id}</Table.Td>
                  <Table.Td>{customer.userName}</Table.Td>
                  <Table.Td>{customer.email}</Table.Td>
                  <Table.Td>{customer.phoneNumber}</Table.Td>
                </Table.Tr>
              ))}
            </Table.Tbody>
          </Table>

          {data && data.length === 0 && (
            <Center my="md">
              <Text fz="md">No customers found</Text>
            </Center>
          )}
        </Paper>

        {/* <Flex justify="flex-end">
          {data?.totalPages && (
            <Pagination
              total={data?.totalPages}
              withEdges
              onChange={handlePaginate}
            />
          )}
        </Flex> */}
      </Stack>
    </Paper>
  );
}
