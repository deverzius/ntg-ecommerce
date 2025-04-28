import { createTheme, rem, type MantineTheme } from "@mantine/core";
import { FontWeight } from "./types/enum";

export const theme = createTheme({
  fontSizes: {
    xs: rem(10),
    sm: rem(12),
    md: rem(14),
    lg: rem(16),
    xl: rem(20),
  },

  components: {
    Paper: {
      styles: () => ({
        bg: "#FFFFFF",
        root: {
          overflow: "hidden",
        },
      }),
    },
    Table: {
      defaultProps: {
        verticalSpacing: "sm",
        horizontalSpacing: "xl",
        borderColor: "gray.1",
      },
      styles: (theme: MantineTheme) => ({
        thead: {
          backgroundColor: theme.colors.gray[0],
        },
        th: {
          fontSize: theme.fontSizes.md,
          fontWeight: FontWeight.Medium,
          color: theme.colors.gray[8],
        },
        td: {
          fontSize: theme.fontSizes.md,
          color: theme.colors.gray[9],
        },
      }),
    },
    Title: {
      defaultProps: {
        size: "h3",
      },
    },
  },
});
