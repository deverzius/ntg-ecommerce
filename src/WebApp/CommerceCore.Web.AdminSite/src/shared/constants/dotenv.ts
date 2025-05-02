type Dotenv = {
  API_URL: string;
};

const env = import.meta.env;

export const dotenv: Dotenv = {
  API_URL: env.VITE_API_URL,
};
