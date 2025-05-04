import { logOutUser } from "@/services/authServices";
import { useMutation } from "@tanstack/react-query";

export function useLogOutMutation() {
  return useMutation({
    mutationFn: logOutUser,
  });
}
