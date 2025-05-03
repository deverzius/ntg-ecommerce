import { useEffect } from "react";
import { Box, Center, Text } from "@mantine/core";
import { FontWeight } from "@/shared/types/enum";
import { useHandleAuthCodeMutation } from "@/hooks/auth/useHandleAuthCodeMutation";

export default function Page() {
  const { mutateAsync } = useHandleAuthCodeMutation();

  useEffect(() => {
    const urlParams = new URLSearchParams(window.location.search);
    const authCode = urlParams.get("code");

    if (authCode) {
      mutateAsync({ authCode });
    }
  }, []);

  return (
    <Box mt="40vh">
      <Center>
        <Text fz="xl" fw={FontWeight.Medium}>
          Authenticating...
        </Text>
      </Center>
    </Box>
  );
}
