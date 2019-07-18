import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment as Environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {
  private actionUrl: string;
  private headers: HttpHeaders;

  constructor(private _http: HttpClient) {
    this.actionUrl = Environment.webApiBaseUrl + '/broker/portfolio';

    this.headers = new HttpHeaders();
    this.headers = this.headers.append('Accept', 'application/json');
    this.headers = this.headers.append('Content-Type', 'application/json');
  }

  public getPortfolio(): Observable<any[]> {
    return this._http
      .get(this.actionUrl, { headers: this.headers })
      .map((response) => <any[]>response)
      .do(_ => { console.log('Get portfolio service call started...'); })
      .catch(this.handleError);
  }

  public getInvestments(): Observable<any[]> {
    return this._http
      .get(this.actionUrl, { headers: this.headers })
      .map((response) => <any[]>response)
      .do(_ => { console.log('Get investments service call started...'); })
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
