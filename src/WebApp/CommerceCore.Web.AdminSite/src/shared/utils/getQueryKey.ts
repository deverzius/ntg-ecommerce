type FnName =
  | "getProducts"
  | "getProductById"
  | "getBrands"
  | "getCategories"
  | "getCategoryById"
  | "getUserInfo";

export function getQueryKey(fnName: FnName, data?: object) {
  const optionalData = data ? Object.values(data) : [];

  switch (fnName) {
    case "getProducts":
      return ["products", ...optionalData];
    case "getProductById":
      return ["product", ...optionalData];

    case "getBrands":
      return ["brands", ...optionalData];

    case "getCategories":
      return ["categories", ...optionalData];
    case "getCategoryById":
      return ["category", ...optionalData];

    case "getUserInfo":
      return ["userInfo", ...optionalData];

    default:
      throw new Error(`Invalid function name: ${fnName}`);
  }
}
