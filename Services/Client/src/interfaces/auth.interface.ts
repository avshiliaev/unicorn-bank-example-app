export interface Auth0User {
    email: string
    email_verified: boolean
    name: string
    nickname: string
    picture: string
    sub: string
    updated_at: string
}

// General
export interface AuthInterface {
    isLoggedIn: boolean
    userId?: string
    username?: string
}

// Authentication Info
export interface AuthReducerState extends AuthInterface {
    loading: boolean
    error: boolean
}

export interface AuthAction {
    type: string
    params?: any
    state: AuthReducerState
}
