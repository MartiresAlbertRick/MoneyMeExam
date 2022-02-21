import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetLoanStatuses<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = this.apiService.BaseUrl + 'common/loan-statuses';
    return this.apiService.GetMany<T>();
  }

  public GetProductTypes<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = this.apiService.BaseUrl + 'common/product-types';
    return this.apiService.GetMany<T>();
  }

  public GetRepaymentFrequencies<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = this.apiService.BaseUrl + 'common/repayment-frequencies';
    return this.apiService.GetMany<T>();
  }
}
