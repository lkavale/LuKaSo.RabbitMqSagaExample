import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, pipe, Subject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoaderIndicatorState } from './loader-indicator';
import { LoaderIndicatorService } from './loader-indicator.service';

@Injectable({
  providedIn: 'root'
})
export class LoaderInterceptorService implements HttpInterceptor {
  private loaderIndicatorSubject = new Subject<LoaderIndicatorState>();
  loaderIndicatorState = this.loaderIndicatorSubject.asObservable();

  constructor(private _loaderIndicatorService: LoaderIndicatorService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this._loaderIndicatorService.start();

    return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        this._loaderIndicatorService.notifySuccess("");
      }
    },
      (err: any) => {
        this._loaderIndicatorService.notifyError(err);
      }));
  }
}
