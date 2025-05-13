export type CreateCategoryRequestDto = {
    name: string;
    description: string;
    parentCategoryId?: string;
};

export type UpdateCategoryRequestDto = {
    id: string;
    name: string;
    description: string;
    parentCategoryId?: string;
};
