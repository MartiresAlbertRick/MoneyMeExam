import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetCustomers<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}customers`;
    return this.apiService.GetMany<T>();
  }

  public GetCustomer<T>(id?: number) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}customers/${id}`;
      return this.apiService.GetOne<T>();
  }

  public CreateCustomer<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}customers`;
    return this.apiService.PostData<T>(body);
  }

  public UpdateCustomer<T>(body: string) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}customers`;
      return this.apiService.PutData<T>(body);
  }
}
