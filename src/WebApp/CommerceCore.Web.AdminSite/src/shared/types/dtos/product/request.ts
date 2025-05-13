export type CreateProductRequestDto = {
    name: string;
    description: string;
    price: number;
    brandId: string;
    categoryId: string;
};

export type UpdateProductRequestDto = {
    id: string;
    name: string;
    description: string;
    price: number;
    brandId: string;
    categoryId: string;
    images: ProductImageRequestDto[];
};

export type ProductImageRequestDto = {
    name: string;
    path: string;
};
