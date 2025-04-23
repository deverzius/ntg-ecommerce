import { AppNavbar } from "@/components/AppNavbar/AppNavbar";
import { HeaderTabs } from "@/components/HeaderTabs/HeaderTabs";
import { Box, Flex } from "@mantine/core";
import { Outlet } from "react-router";

export default function MainLayout() {
  return (
    <Flex direction="column" mih="100vh">
      <HeaderTabs />
      <Flex direction="row" flex={1}>
        <AppNavbar />
        <Box flex={1} bg="gray.0" p="md">
          <Outlet />
        </Box>
      </Flex>
    </Flex>
  );
}
