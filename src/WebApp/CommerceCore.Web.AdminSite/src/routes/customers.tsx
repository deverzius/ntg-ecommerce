import {default as Page} from "@/pages/customers";
import type {Route} from "../+types/root";

export function meta({}: Route.MetaArgs) {
    return [
        {title: "New React Router App"},
        {name: "description", content: "Welcome to React Router!"},
    ];
}

export default function Route() {
    return <Page/>;
}
