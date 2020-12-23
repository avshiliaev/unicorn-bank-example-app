import {HubConnection, IStreamResult, IStreamSubscriber, ISubscription} from "@microsoft/signalr";

export const createMockSocket = (): HubConnection => {
    const socket: {
        stop(): Promise<void>;
        stopInternal(error?: Error): Promise<void>;
        stream<T = any>(methodName: string, ...args: any[]): IStreamResult<T>;
        onclose(callback: (error?: Error) => void): void;
        onreconnected(callback: (connectionId?: string) => void): void;
        start(): Promise<void>;
        invoke<T = any>(methodName: string, ...args: any[]): Promise<T>;
        onreconnecting(callback: (error?: Error) => void): void;
        send(methodName: string, ...args: any[]): Promise<void>;
        off(methodName: string, method?: (...args: any[]) => void): void;
        on(methodName: string, newMethod: (...args: any[]) => void): void
    } = {

        invoke<T = any>(methodName: string, ...args: any[]): Promise<T> {
            return Promise.resolve(undefined);
        },
        off(methodName: string, method?: (...args: any[]) => void): void {
        },
        on(methodName: string, newMethod: (...args: any[]) => void): void {
        },
        onclose(callback: (error?: Error) => void): void {
        },
        onreconnected(callback: (connectionId?: string) => void): void {
        },
        onreconnecting(callback: (error?: Error) => void): void {
        },
        send(methodName: string, ...args: any[]): Promise<void> {
            return Promise.resolve(undefined);
        },
        start(): Promise<void> {
            return Promise.resolve(undefined);
        },
        stop(): Promise<void> {
            return Promise.resolve(undefined);
        },
        stopInternal(error?: Error): Promise<void>{
            return Promise.resolve(undefined);
        },
        stream<T = any>(methodName: string, ...args: any[]): IStreamResult<T> {
            return new class implements IStreamResult<T> {
                subscribe(subscriber: IStreamSubscriber<T>): ISubscription<T> {
                    return new class implements ISubscription<T> {
                        dispose(): void {
                        }
                    };
                }
            };
        }
    }

    return <HubConnection><unknown>socket;
}