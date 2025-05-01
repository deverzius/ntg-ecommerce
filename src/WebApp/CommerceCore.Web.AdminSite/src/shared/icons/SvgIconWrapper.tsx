import type { SvgProps } from "@/types/svgProps";

export function SvgIconWrapper({
  children,
  props,
}: {
  children: React.ReactNode;
  props: SvgProps;
}) {
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width={props.size ?? 24}
      height={props.size ?? 24}
      viewBox="0 0 24 24"
      fill="none"
      stroke={props.strokeColor ?? "currentColor"}
      strokeWidth={props.strokeWidth ?? 1.5}
      strokeLinecap="round"
      strokeLinejoin="round"
      className={props.className ?? ""}
    >
      {children}
    </svg>
  );
}
