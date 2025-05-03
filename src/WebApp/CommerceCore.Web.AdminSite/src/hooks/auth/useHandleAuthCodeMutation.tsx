import { fetchToken } from "@/services/authServices";
import { dotenv } from "@/shared/constants/dotenv";
import { setCookie } from "@/shared/utils/setCookie";
import { useMutation } from "@tanstack/react-query";

type MutationFn = {
  authCode: string;
};

export function useHandleAuthCodeMutation() {
  return useMutation({
    mutationFn: async ({ authCode }: MutationFn) => {
      fetchToken(authCode)
        .then((res) => {
          setCookie("access_token", res.access_token, res.expires_in);
        })
        .then(() => {
          const timeoutId = setTimeout(() => {
            window.location.href = `${dotenv.CLIENT_URL}`;
            clearTimeout(timeoutId);
          }, 2000);
        });
    },
  });
}
