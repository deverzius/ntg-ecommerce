type Dotenv = {
    API_URL: string;
    IDENTITY_SERVER_URL: string;
    CLIENT_ID: string;
    CLIENT_SECRET: string;
    ADMIN_URL: string;
    SUPABASE_STORAGE_URL: string;
};

const env = import.meta.env;

export const dotenv: Dotenv = {
    ADMIN_URL: env.VITE_ADMIN_URL,
    API_URL: env.VITE_API_URL,
    IDENTITY_SERVER_URL: env.VITE_IDENTITY_SERVER_URL,
    CLIENT_ID: env.VITE_CLIENT_ID,
    CLIENT_SECRET: env.VITE_CLIENT_SECRET,
    SUPABASE_STORAGE_URL: env.VITE_SUPABASE_STORAGE_URL,
};
