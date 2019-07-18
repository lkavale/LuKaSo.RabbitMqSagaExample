import { Injectable } from '@angular/core';
import { environment as Environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';
import { StrategyApiService } from './strategy-api.service';

@Injectable({
  providedIn: 'root'
})
export class StrategyApiProviderService {
  constructor(private _http: HttpClient) {
  }

  public getService(strategyAddress: string): StrategyApiService {
    return new StrategyApiService(this._http, strategyAddress);
  }
}
