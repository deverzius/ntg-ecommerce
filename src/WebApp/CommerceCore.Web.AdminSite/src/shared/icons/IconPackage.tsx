import type { SvgProps } from "@/types/svg-props";
import { SvgIconWrapper } from "./SvgIconWrapper";

export function IconPackage(props: SvgProps) {
  return (
    <SvgIconWrapper props={props}>
      <path stroke="none" d="M0 0h24v24H0z" fill="none" />
      <path d="M12 3l8 4.5l0 9l-8 4.5l-8 -4.5l0 -9l8 -4.5" />
      <path d="M12 12l8 -4.5" />
      <path d="M12 12l0 9" />
      <path d="M12 12l-8 -4.5" />
      <path d="M16 5.25l-8 4.5" />
    </SvgIconWrapper>
  );
}
