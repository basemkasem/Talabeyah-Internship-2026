import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';

@Service()
export class AuthService {
    private readonly apiUrl: string = 'https://localhost:7229/api/login';

    private http = inject(HttpClient);

    login(username: string, password: string) : Observable<string>{
        return this.http.post<string>( this.apiUrl, {username, password} );
    }
}
