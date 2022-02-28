import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiService } from 'src/app/services/services';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  constructor(
    private apiService : ClientApiService
  ) { }

  public GetLoans<T>() : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}loans`;
    return this.apiService.GetMany<T>();
  }

  public GetLoan<T>(id?: number) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}loans/${id}`;
      return this.apiService.GetOne<T>();
  }

  public CreateLoan<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}loans`;
    return this.apiService.PostData<T>(body);
  }

  public UpdateLoan<T>(body: string, validateApplication ?: boolean) : Observable<T> {
      this.apiService.NormalHeader();
      this.apiService.IncludeValidateApplicationHeader((validateApplication || false).toString());
      this.apiService.ApiUrl = `${this.apiService.BaseUrl}loans`;
      return this.apiService.PutData<T>(body);
  }

  public ComputeLoanRepayments<T>(body: string) : Observable<T> {
    this.apiService.NormalHeader();
    this.apiService.ApiUrl = `${this.apiService.BaseUrl}loans/compute-loan-repayments`;
    return this.apiService.PutData<T>(body);
  }
}
