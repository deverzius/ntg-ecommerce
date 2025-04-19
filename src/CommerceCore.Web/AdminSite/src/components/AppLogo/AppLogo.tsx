import { Image } from "@mantine/core";

interface AppLogoProps {
  width?: number;
}

export function AppLogo({ width }: AppLogoProps) {
  return <Image src="images/logo.svg" alt="app-logo" w={width} />;
}
