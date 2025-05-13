import {dotenv} from "@/shared/constants/dotenv";
import type {UserResponseDto} from "@/shared/types/dtos/auth/response";

export async function getAllCustomers(): Promise<UserResponseDto[]> {
    return await fetch(`${dotenv.IDENTITY_SERVER_URL}/customers/list`, {
        method: "GET",
        headers: {
            Accept: "application/json",
        },
        credentials: "include",
    }).then((res) => res.json());
}
