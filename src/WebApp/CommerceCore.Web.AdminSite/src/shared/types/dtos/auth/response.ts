export type AuthResponseDto = {
  access_token: string;
  token_type: string;
  expires_in: number;
  scope: string;
  id_token: string;
  refresh_token: string;
};

export type UserResponseDto = {
  id: string;
  email: string;
  userName: string;
  phoneNumber?: string;
  role?: string;
};
