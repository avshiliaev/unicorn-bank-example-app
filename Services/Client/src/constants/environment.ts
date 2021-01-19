const Environment = {

    // Paths
    PATHS_WS: "http://localhost:80",
    PATHS_PROFILES: process.env.REACT_APP_PATHS_PROFILES ?? "/api/profiles",
    PATHS_NOTIFICATIONS: process.env.REACT_APP_PATHS_NOTIFICATIONS ?? "/api/notifications",

    // Authentication / Authorization
    AUTH_DOMAIN: process.env.REACT_APP_DOMAIN ?? "",
    AUTH_CLIENT_ID: process.env.REACT_APP_CLIENT_ID ?? "",
    AUTH_AUDIENCE: process.env.REACT_APP_AUDIENCE ?? "",
};

export {Environment};
