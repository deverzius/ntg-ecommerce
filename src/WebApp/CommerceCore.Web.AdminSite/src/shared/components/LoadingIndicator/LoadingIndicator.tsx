import { Loader } from "@mantine/core";

interface LoadingIndicatorProps {
  size?: number;
}

export function LoadingIndicator({ size = 32 }: LoadingIndicatorProps) {
  return <Loader size={size} />;
}
