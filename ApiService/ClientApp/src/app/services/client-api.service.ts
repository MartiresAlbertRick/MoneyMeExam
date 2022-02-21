import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ClientApiService {
    public ApiUrl = window.location.origin + '/api/';
    public HttpHeaders : HttpHeaders = new HttpHeaders({});

    constructor(private httpClient : HttpClient) { }

    public NormalHeader() {
        this.HttpHeaders = new HttpHeaders({
            'Content-Type': 'application/json'
        });
    }

    public GetMany<T>() : Observable<T> {
        return this.httpClient.get<T>(this.ApiUrl, { headers: this.HttpHeaders });
    }

    public GetOne<T>() : Observable<T> {
        return this.httpClient.get<T>(this.ApiUrl, { headers: this.HttpHeaders });
    }

    public PostData<T>(body : string) : Observable<T>{
        return this.httpClient.post<T>(this.ApiUrl, body, { headers : this.HttpHeaders });
    }

    public PutData<T>(body : string) : Observable<T>{
        return this.httpClient.put<T>(this.ApiUrl, body, { headers : this.HttpHeaders });
    }
}