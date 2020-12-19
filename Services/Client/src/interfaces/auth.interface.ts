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
