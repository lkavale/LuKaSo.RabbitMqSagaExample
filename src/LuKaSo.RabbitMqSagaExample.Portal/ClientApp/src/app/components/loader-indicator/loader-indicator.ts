export interface LoaderIndicatorState {
  visible: boolean;
}

export interface Responsive {
  notifySuccess(message: string): void;
  notifyError(error: string): void;
}

export interface Initiable {
  start(): void;
}
