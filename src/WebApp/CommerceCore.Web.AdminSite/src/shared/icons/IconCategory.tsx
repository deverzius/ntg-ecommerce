import type { SvgProps } from "@/types/svg-props";
import { SvgIconWrapper } from "./SvgIconWrapper";

export function IconCategory(props: SvgProps) {
  return (
    <SvgIconWrapper props={props}>
      <path stroke="none" d="M0 0h24v24H0z" fill="none" />
      <path d="M4 4h6v6h-6z" />
      <path d="M14 4h6v6h-6z" />
      <path d="M4 14h6v6h-6z" />
      <path d="M17 17m-3 0a3 3 0 1 0 6 0a3 3 0 1 0 -6 0" />
    </SvgIconWrapper>
  );
}
