import {index, layout, route, type RouteConfig,} from "@react-router/dev/routes";

export default [
    route("callback", "routes/callback.tsx"),
    layout("shared/layouts/MainLayout.tsx", [
        index("routes/customers.tsx"),
        route("products", "routes/products.tsx"),
        route("categories", "routes/categories.tsx"),
    ]),
] satisfies RouteConfig;
