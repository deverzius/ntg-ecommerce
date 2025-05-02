import {
  type RouteConfig,
  index,
  layout,
  route,
} from "@react-router/dev/routes";

export default [
  layout("shared/layouts/MainLayout.tsx", [
    index("routes/customers.tsx"),
    route("products", "routes/products.tsx"),
    route("categories", "routes/categories.tsx"),
  ]),
] satisfies RouteConfig;
