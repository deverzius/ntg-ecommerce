import { IconCategory, IconPackage, IconUsers } from "@tabler/icons-react";
import { Text, Box, NavLink } from "@mantine/core";
import classes from "./AppNavbar.module.css";
import { FontWeight } from "@/types/enum";
import { useLocation } from "react-router";

const data = [
  { link: "/", label: "Customers", icon: IconUsers },
  { link: "/products", label: "Products", icon: IconPackage },
  { link: "/categories", label: "Categories", icon: IconCategory },
];

export function AppNavbar() {
  const location = useLocation();

  const links = data.map((item) => (
    <NavLink
      className={classes.link}
      data-active={location.pathname === item.link || undefined}
      href={item.link}
      key={item.label}
      leftSection={<item.icon className={classes.linkIcon} stroke={1.5} />}
      label={<Text fw={FontWeight.Medium}>{item.label}</Text>}
    />
  ));

  return (
    <nav className={classes.navbar}>
      <Box flex={1}>{links}</Box>
    </nav>
  );
}
