import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { LoaderIndicatorState, Responsive, Initiable } from './loader-indicator';

@Injectable()

export class LoaderIndicatorService implements Responsive, Initiable {
  private loaderIndicatorSubject = new Subject<LoaderIndicatorState>();
  loaderIndicatorState = this.loaderIndicatorSubject.asObservable();

  constructor() {
  }

  start(): void {
    this.loaderIndicatorSubject.next(<LoaderIndicatorState>{ visible: true });
  }

  notifySuccess(message: string): void {
    this.loaderIndicatorSubject.next(<LoaderIndicatorState>{ visible: false });
  }

  notifyError(error: string): void {
    this.loaderIndicatorSubject.next(<LoaderIndicatorState>{ visible: false });
  }
}
