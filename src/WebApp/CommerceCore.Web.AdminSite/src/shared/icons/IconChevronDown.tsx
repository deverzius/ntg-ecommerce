import type { SvgProps } from "@/shared/types/SvgProps";
import { SvgIconWrapper } from "./SvgIconWrapper";

export function IconChevronDown(props: SvgProps) {
  return (
    <SvgIconWrapper props={props}>
      <path stroke="none" d="M0 0h24v24H0z" fill="none" />
      <path d="M6 9l6 6l6 -6" />
    </SvgIconWrapper>
  );
}
