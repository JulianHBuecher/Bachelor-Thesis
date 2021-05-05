import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public oidcSecurityService: OidcSecurityService, public http: HttpClient) { }

  public get(url: string): Observable<any> {
    return this.http.get<any>(url, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        return throwError(error);
      }))
  }

  public put(url: string, data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http.put(url, body, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        return throwError(error);
      }));
  }

  public delete(url: string): Observable<any> {
    return this.http.delete(url, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        return throwError(error);
      }));
  }

  public post(url: string, data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http.post(url, body, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        return throwError(error);
      }));
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    this.oidcSecurityService.forceRefreshSession().subscribe((result) => console.log(result));
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  private getHeaders() {
    let headers = new HttpHeaders();headers = headers.set('Content-Type', 'application/json');
    return this.appendAuthHeader(headers);
  }

  public getToken() {
    const token = this.oidcSecurityService.getToken();
    return token;
  }

  private appendAuthHeader(headers: HttpHeaders) {
    const token = this.oidcSecurityService.getToken();

    if (token === '') { return headers; }

    const tokenHeader = 'Bearer ' + token;
    return headers.set('Authorization', tokenHeader);
  }
}
