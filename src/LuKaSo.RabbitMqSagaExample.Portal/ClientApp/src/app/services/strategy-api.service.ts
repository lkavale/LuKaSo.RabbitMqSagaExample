import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment as Environment } from './../../environments/environment';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';
import { retry } from 'rxjs/operators';

export class StrategyApiService{
  private actionUrl: string;
  private headers: HttpHeaders;

  constructor(protected _http: HttpClient, strategyAddress: string) {
    this.actionUrl = Environment.webApiBaseUrl + '/' + strategyAddress + "/configuration";

    this.headers = new HttpHeaders();
    this.headers = this.headers.append('Accept', 'application/json');
    this.headers = this.headers.append('Content-Type', 'application/json');
  }

  public save(config: any): Observable<any> {
    let toAdd = JSON.stringify(config);

    return this._http
      .put(this.actionUrl, toAdd, { headers: this.headers })
      .do(_ => { console.log('Save config service call started...'); })
      .catch(this.handleError);
  }

  public get(): Observable<any> {
    return this._http
      .get(this.actionUrl, { headers: this.headers })
      .do(_ => { console.log('Get config service call started...'); })
      .catch(this.handleError);
  }

  private handleError(error: any) {
    var applicationError = error.headers.get('Application-Error');
    var serverError = error.json();
    var modelStateErrors: string = '';

    if (!serverError.type) {
      console.log(serverError);
      for (var key in serverError) {
        if (serverError[key])
          modelStateErrors += serverError[key] + '\n';
      }
    }

    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;

    return Observable.throw(applicationError || modelStateErrors || 'Server error');
  }
}
