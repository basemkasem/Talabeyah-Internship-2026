import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Service()
export class AuthService {
    private readonly apiUrl: string = environment.apiUrl + 'user/login';

    private http = inject(HttpClient);

    login(params: LoginRequest) : Observable<string>{
        return this.http.post<string>(this.apiUrl, params, {responseType: 'text'}).pipe(
            tap((value) => localStorage.setItem( 'userToken', value))
        );
    }
}
