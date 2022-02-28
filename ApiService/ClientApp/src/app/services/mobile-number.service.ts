import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class MobileNumberService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetMobileNumbers<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}mobile-numbers`;
    return this.apiService.GetMany<T>();
  }

  public GetMobileNumber<T>(id?: number) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}mobile-numbers/${id}`;
      return this.apiService.GetOne<T>();
  }

  public CreateMobileNumber<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}mobile-numbers`;
    return this.apiService.PostData<T>(body);
  }

  public UpdateMobileNumber<T>(body: string) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}mobile-numbers`;
      return this.apiService.PutData<T>(body);
  }
}
