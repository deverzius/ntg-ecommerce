import {
  Autocomplete,
  Avatar,
  Box,
  Burger,
  Group,
  Menu,
  Text,
  UnstyledButton,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import classes from "./HeaderMenu.module.css";
import { AppLogo } from "../AppLogo/AppLogo";
import { IconSearch } from "@/shared/icons/IconSearch";
import { IconChevronDown } from "@/shared/icons/IconChevronDown";
import { IconLogout } from "@/shared/icons/IconLogout";
import type { UserResponseDto } from "@/shared/types/dtos/auth/response";
import { useLogOutMutation } from "@/hooks/auth/useLogOutMutation";
import { useEffect } from "react";
import { useAuthorizeUserMutation } from "@/hooks/auth/useAuthorizeUserMutation";

interface HeaderMenuProps {
  user?: UserResponseDto;
}

export function HeaderMenu({ user }: HeaderMenuProps) {
  const { data: logOutResponse, mutateAsync: logout } = useLogOutMutation();
  const { mutateAsync: authorizeUser } = useAuthorizeUserMutation();
  const [opened, { toggle }] = useDisclosure(false);

  useEffect(() => {
    if (logOutResponse?.ok) {
      authorizeUser();
    }
  }, [logOutResponse]);

  return (
    <Box className={classes.headerTabsWrapper} bg="white" p="md">
      <Group justify="space-between" align="center">
        <AppLogo width={150} />

        <Group>
          <Autocomplete
            placeholder="Search"
            w={400}
            leftSection={<IconSearch size={16} />}
            data={[]}
            visibleFrom="xs"
          />
        </Group>

        <Burger opened={opened} onClick={toggle} hiddenFrom="xs" size="sm" />

        <Menu
          width={260}
          position="bottom-end"
          transitionProps={{ transition: "pop-top-right" }}
          withinPortal
        >
          <Menu.Target>
            <UnstyledButton>
              <Group gap={7}>
                <Avatar alt={user?.email} radius="xl" size={40} color="blue">
                  {user?.email.substring(0, 2).toUpperCase()}
                </Avatar>
                <Text fw={500} lh={1} mr={3}>
                  {user?.email}
                </Text>
                <IconChevronDown size={20} />
              </Group>
            </UnstyledButton>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Label>Settings</Menu.Label>
            <Menu.Item
              leftSection={<IconLogout size={16} />}
              onClick={() => logout()}
            >
              Logout
            </Menu.Item>
          </Menu.Dropdown>
        </Menu>
      </Group>
    </Box>
  );
}
