export function setCookie(
    cookieName: string,
    cookieValue: string,
    expiresInSeconds: number
) {
    const date = new Date();
    date.setTime(date.getTime() + expiresInSeconds * 1000);

    const expires = "expires=" + date.toUTCString();

    document.cookie = `${cookieName}=${cookieValue}; ${expires}; path=/; Secure`;
}
