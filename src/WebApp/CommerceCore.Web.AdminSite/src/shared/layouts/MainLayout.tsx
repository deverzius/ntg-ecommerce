import { useGetUserInfoQuery } from "@/hooks/auth/useGetUserInfoQuery";
import { useAuthorizeUserMutation } from "@/hooks/auth/useAuthorizeUserMutation";
import { AppNavbar } from "@/shared/components/AppNavbar/AppNavbar";
import { HeaderMenu } from "@/shared/components/HeaderMenu/HeaderMenu";
import { Box, Center, Flex } from "@mantine/core";
import { useEffect } from "react";
import { Outlet } from "react-router";
import { LoadingIndicator } from "../components/LoadingIndicator/LoadingIndicator";

export default function MainLayout() {
  const { isLoading, data: user } = useGetUserInfoQuery();
  const { mutateAsync: authorizeAsync } = useAuthorizeUserMutation();

  useEffect(() => {
    if (isLoading) {
      return;
    }
    if (user?.role === "Admin") {
      return;
    }
    authorizeAsync();
  });

  if (isLoading) {
    return (
      <Box mt="40vh">
        <Center>
          <LoadingIndicator />
        </Center>
      </Box>
    );
  }

  return (
    <Flex direction="column" mih="100vh">
      <HeaderMenu user={user} />
      <Flex direction="row" flex={1}>
        <AppNavbar />
        <Box flex={1} bg="gray.0" p="md">
          <Outlet />
        </Box>
      </Flex>
    </Flex>
  );
}
