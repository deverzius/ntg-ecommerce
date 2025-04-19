import { useState } from "react";
import { IconCategory, IconPackage, IconUsers } from "@tabler/icons-react";
import { Text, Box, NavLink } from "@mantine/core";
import classes from "./AppNavbar.module.css";
import { FontWeight } from "@/types/enum";

const data = [
  { link: "", label: "Customers", icon: IconUsers },
  { link: "", label: "Products", icon: IconPackage },
  { link: "", label: "Categories", icon: IconCategory },
];

export function AppNavbar() {
  const [active, setActive] = useState("Customers");

  const links = data.map((item) => (
    <NavLink
      className={classes.link}
      data-active={item.label === active || undefined}
      href={item.link}
      key={item.label}
      onClick={(event) => {
        event.preventDefault();
        setActive(item.label);
      }}
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
