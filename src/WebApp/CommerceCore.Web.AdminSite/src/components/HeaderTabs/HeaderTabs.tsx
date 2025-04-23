import { useState } from "react";
import {
  IconChevronDown,
  IconLogout,
  IconSearch,
  IconSettings,
  IconSwitchHorizontal,
  IconTrash,
} from "@tabler/icons-react";
import {
  Autocomplete,
  Avatar,
  Box,
  Burger,
  Group,
  Menu,
  Text,
  UnstyledButton,
  useMantineTheme,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import classes from "./HeaderTabs.module.css";
import { AppLogo } from "../AppLogo/AppLogo";

const user = {
  name: "Jane Spoonfighter",
  email: "janspoon@fighter.dev",
  image:
    "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-5.png",
};

export function HeaderTabs() {
  const theme = useMantineTheme();
  const [opened, { toggle }] = useDisclosure(false);
  const [userMenuOpened, setUserMenuOpened] = useState(false);

  return (
    <Box className={classes.headerTabsWrapper} bg="white" p="md">
      <Group justify="space-between" align="center">
        <AppLogo width={150} />

        <Group>
          <Autocomplete
            placeholder="Search"
            w={400}
            leftSection={<IconSearch size={16} stroke={1.5} />}
            data={[
              "React",
              "Angular",
              "Vue",
              "Next.js",
              "Riot.js",
              "Svelte",
              "Blitz.js",
            ]}
            visibleFrom="xs"
          />
        </Group>

        <Burger opened={opened} onClick={toggle} hiddenFrom="xs" size="sm" />

        <Menu
          width={260}
          position="bottom-end"
          transitionProps={{ transition: "pop-top-right" }}
          onClose={() => setUserMenuOpened(false)}
          onOpen={() => setUserMenuOpened(true)}
          withinPortal
        >
          <Menu.Target>
            <UnstyledButton>
              <Group gap={7}>
                <Avatar
                  src={user.image}
                  alt={user.name}
                  radius="xl"
                  size={20}
                />
                <Text fw={500} size="sm" lh={1} mr={3}>
                  {user.name}
                </Text>
                <IconChevronDown size={12} stroke={1.5} />
              </Group>
            </UnstyledButton>
          </Menu.Target>

          <Menu.Dropdown>
            <Menu.Label>Settings</Menu.Label>
            <Menu.Item leftSection={<IconSettings size={16} stroke={1.5} />}>
              Account settings
            </Menu.Item>
            <Menu.Item
              leftSection={<IconSwitchHorizontal size={16} stroke={1.5} />}
            >
              Change account
            </Menu.Item>
            <Menu.Item leftSection={<IconLogout size={16} stroke={1.5} />}>
              Logout
            </Menu.Item>

            <Menu.Divider />

            <Menu.Label>Danger zone</Menu.Label>
            <Menu.Item
              color="red"
              leftSection={<IconTrash size={16} stroke={1.5} />}
            >
              Delete account
            </Menu.Item>
          </Menu.Dropdown>
        </Menu>
      </Group>
    </Box>
  );
}
