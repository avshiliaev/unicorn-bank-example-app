// General
export interface NotificationInterface {
    description: string;
    profile: string;
    status: string;
    time: string;
    title: string
    uuid: string;
}

// Notifications
export interface NotificationsReducerState {
    loading: boolean,
    error: boolean,
    data?: NotificationInterface[]
}

export interface NotificationsAction {
    type: string,
    params?: any
    state: NotificationsReducerState,
}
