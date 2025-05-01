type FnName = "getProducts" | "getProductById" | "getBrands";

export function getQueryKey(fnName: FnName, data?: object) {
  const optionalData = data ? Object.values(data) : [];

  switch (fnName) {
    case "getProducts":
      return ["products", ...optionalData];
    case "getProductById":
      return ["product", ...optionalData];
    case "getBrands":
      return ["brands", ...optionalData];
    default:
      throw new Error(`Invalid function name: ${fnName}`);
  }
}
