import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from '../services/token.service';

export const TokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = inject(TokenService).getToken();
  if (authToken)
  {
    const newReq = req.clone({
      headers: req.headers.append('Authorization', `Bearer ${authToken}`)
    });
    return next(newReq);
  }
  return next(req);
};
