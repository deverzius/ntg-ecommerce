import { authorizeUser } from "@/services/authServices";
import { useMutation } from "@tanstack/react-query";

export function useAuthorizeUserMutation() {
  return useMutation({
    mutationFn: async () => {
      const res = await authorizeUser();

      if (res.redirected) {
        window.location.href = res.url;
      }
    },
  });
}
