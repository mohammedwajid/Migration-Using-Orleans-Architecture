import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = 'http://localhost:6001/api/auth';
  private tokenKey = 'jwt_token';

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<{ token: string }>(`${this.api}/login`, { username, password })
      .pipe(tap(res => localStorage.setItem(this.tokenKey, res.token)));
  }

  getToken(): string | null { return localStorage.getItem(this.tokenKey); }
}
