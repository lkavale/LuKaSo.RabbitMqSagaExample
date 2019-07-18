import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment as Environment } from './../../environments/environment';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';

@Injectable({
  providedIn: 'root'
})
export class InvestmentService {
  private actionUrl: string;
  private headers: HttpHeaders;

  constructor(private _http: HttpClient) {
    this.actionUrl = Environment.webApiBaseUrl + '/broker/investment';

    this.headers = new HttpHeaders();
    this.headers = this.headers.append('Accept', 'application/json');
    this.headers = this.headers.append('Content-Type', 'application/json');
  }

  public addInvestment(investment: any): Observable<any> {
    let toAdd = JSON.stringify(investment);

    return this._http
      .post(this.actionUrl, toAdd, { headers: this.headers })
      .do(_ => { console.log('Add investment service call started...'); })
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
