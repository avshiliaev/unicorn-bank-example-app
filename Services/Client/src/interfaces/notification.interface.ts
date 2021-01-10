// General
export interface NotificationInterface {
    description: string
    profileId: string
    status: string
    timeStamp: string
    title: string
    id: string
    version: number
}

// Notifications
export interface NotificationsReducerState {
    loading: boolean,
    error: boolean,
    version: number,
    data?: NotificationInterface[]
}

export interface NotificationsAction {
    type: string,
    params?: any
    state: NotificationsReducerState,
}
