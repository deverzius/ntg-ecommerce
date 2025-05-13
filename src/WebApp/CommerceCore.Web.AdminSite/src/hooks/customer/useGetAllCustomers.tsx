import {getAllCustomers} from "@/services/customerServices";
import {getQueryKey} from "@/shared/utils/getQueryKey";
import {useQuery} from "@tanstack/react-query";

export function useGetAllCustomersQuery() {
    return useQuery({
        queryKey: getQueryKey("getAllCustomers"),
        queryFn: getAllCustomers,
    });
}
