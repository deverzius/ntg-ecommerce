import type {SvgProps} from "@/shared/types/SvgProps";
import {SvgIconWrapper} from "./SvgIconWrapper";

export function IconPlus(props: SvgProps) {
    return (
        <SvgIconWrapper props={props}>
            <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
            <path d="M12 5l0 14"/>
            <path d="M5 12l14 0"/>
        </SvgIconWrapper>
    );
}
