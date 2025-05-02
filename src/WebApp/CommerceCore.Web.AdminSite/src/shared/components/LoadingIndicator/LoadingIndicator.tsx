import { Loader } from "@mantine/core";

interface LoadingIndicatorProps {
  size?: number;
}

export function LoadingIndicator({ size = 30 }: LoadingIndicatorProps) {
  return <Loader size={size} />;
}
