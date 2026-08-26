import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { Product } from '../interfaces/product.interface';

@Service()
export class ProductService {
  private readonly apiUrl: string = environment.apiUrl + 'product/';

  private http = inject(HttpClient);

  getProducts(pageNumber: number, pageSize: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl, { params: { pageNumber, pageSize } });
  }

  //TODO: Create a service to check a product quantity before increase.
  addProductItemToLocalStorage(productId: string): Observable<number> {
    return this.http.get<number>(this.apiUrl + productId + '/quantity', { params: { productId } });
  }
}
