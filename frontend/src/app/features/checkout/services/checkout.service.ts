import { inject, Service } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CheckoutPayload } from '../interfaces/checkout-payload.interface';
import { TokenService } from '../../../shared/services/token.service';


@Service()
export class CheckoutService {
  private readonly apiUrl: string = environment.apiUrl + `order`;

  private http = inject(HttpClient);
  private userToken = inject(TokenService).getToken();
  private router = inject(Router);

  createOrder(checkoutPayload: CheckoutPayload) {
    const body = {
      orderProducts: checkoutPayload.products,
    };
    if (!this.userToken) {
      this.router.navigate(['/login']);
    }

    const token = this.userToken as string;
    return this.http.post(this.apiUrl, body, {params: {token}});
  }
}
