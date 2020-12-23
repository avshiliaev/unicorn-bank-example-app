import {AuthState} from "@auth0/auth0-react";


declare module "@auth0/auth0-react" {

    export interface Auth0User {
        email: string
        email_verified: boolean
        name: string
        nickname: string
        picture: string
        sub: string
        updated_at: string
    }

    export interface AuthState {
        error?: Error;
        isAuthenticated: boolean;
        isLoading: boolean;
        user?: Auth0User;
    }
}
