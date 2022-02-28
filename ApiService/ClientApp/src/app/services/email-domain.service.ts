import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class EmailDomainService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetEmailDomains<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}email-domains`;
    return this.apiService.GetMany<T>();
  }

  public GetEmailDomain<T>(id?: number) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}email-domains/${id}`;
      return this.apiService.GetOne<T>();
  }

  public CreateEmailDomain<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}email-domains`;
    return this.apiService.PostData<T>(body);
  }

  public UpdateEmailDomain<T>(body: string) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}email-domains`;
      return this.apiService.PutData<T>(body);
  }
}
