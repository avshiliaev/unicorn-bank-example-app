// General
export interface ViewSettings {
    notificationsCount: number
    currentSender: string
}

// View Reducer State
export interface ViewSettingsState extends ViewSettings {
    loading: boolean
    error: boolean
}

export interface ViewSettingsAction {
    type: string
    params?: any
    state: ViewSettingsState
}
