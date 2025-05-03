import { useGetUserInfoQuery } from "@/hooks/auth/useGetUserInfoQuery";
import { useLoginUserMutation } from "@/hooks/auth/useLoginUserMutation";
import { AppNavbar } from "@/shared/components/AppNavbar/AppNavbar";
import { HeaderMenu } from "@/shared/components/HeaderMenu/HeaderMenu";
import { Box, Center, Flex } from "@mantine/core";
import { useEffect } from "react";
import { Outlet } from "react-router";
import { LoadingIndicator } from "../components/LoadingIndicator/LoadingIndicator";

export default function MainLayout() {
  const { isLoading, data } = useGetUserInfoQuery();
  const { mutateAsync: loginAsync } = useLoginUserMutation();

  useEffect(() => {
    if (isLoading) {
      return;
    }
    if (data && data.status === 200) {
      return;
    }
    loginAsync();
  }, [data]);

  if (isLoading || data?.status !== 200) {
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
      <HeaderMenu />
      <Flex direction="row" flex={1}>
        <AppNavbar />
        <Box flex={1} bg="gray.0" p="md">
          <Outlet />
        </Box>
      </Flex>
    </Flex>
  );
}
