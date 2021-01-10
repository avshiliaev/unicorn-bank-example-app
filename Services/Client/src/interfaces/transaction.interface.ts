export interface TransactionInterface {
    id: string
    version: number
    accountId: string
    profileId: string
    amount: number
    info: string
    timestamp: string
    approved: boolean
    pending: boolean
    blocked: boolean
    sequentialNumber: number
}
