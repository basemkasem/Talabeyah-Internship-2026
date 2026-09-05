import { Service } from '@angular/core';

@Service()
export class TokenService {
  private token_key = 'userToken';
  public setToken(token: string) {
    localStorage.setItem(this.token_key, token);
  }

  public getToken() {
    return localStorage.getItem(this.token_key);
  }

  public removeToken() {
    localStorage.removeItem(this.token_key);
  }
}
