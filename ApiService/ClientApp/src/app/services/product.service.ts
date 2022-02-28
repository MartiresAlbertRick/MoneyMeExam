import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetProducts<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}products`;
    return this.apiService.GetMany<T>();
  }

  public GetProduct<T>(id?: number) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}products/${id}`;
      return this.apiService.GetOne<T>();
  }

  public CreateProduct<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}products`;
    return this.apiService.PostData<T>(body);
  }

  public UpdateProduct<T>(body: string) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}products`;
      return this.apiService.PutData<T>(body);
  }
}
