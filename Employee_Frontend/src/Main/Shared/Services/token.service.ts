import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private TOKEN_KEY = 'accessToken';
  constructor() { }

  public setToken(token: string): void {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }
  // Retrieve token
  public getToken(): string | null {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }
  // Remove token
  public removeToken(): void {
    sessionStorage.removeItem(this.TOKEN_KEY);
  }
}
