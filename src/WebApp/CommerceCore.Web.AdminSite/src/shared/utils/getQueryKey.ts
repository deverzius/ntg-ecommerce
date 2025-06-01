type FnName =
  | "getProducts"
  | "getProductById"
  | "getCategories"
  | "getCategoryById"
  | "getUserInfo"
  | "getFileUrl"
  | "getAllCustomers"
  | "getPublicFileUrls";

export function getQueryKey(fnName: FnName, data?: object) {
  const optionalData = data ? Object.values(data) : [];

  switch (fnName) {
    case "getProducts":
      return ["products", ...optionalData];
    case "getProductById":
      return ["product", ...optionalData];

    case "getCategories":
      return ["categories", ...optionalData];
    case "getCategoryById":
      return ["category", ...optionalData];

    case "getUserInfo":
      return ["userInfo", ...optionalData];

    case "getFileUrl":
      return ["fileUrl", ...optionalData];

    case "getAllCustomers":
      return ["customers", ...optionalData];

    case "getPublicFileUrls":
      return ["publicFileUrls", ...optionalData];

    default:
      throw new Error(`Invalid function name: ${fnName}`);
  }
}
