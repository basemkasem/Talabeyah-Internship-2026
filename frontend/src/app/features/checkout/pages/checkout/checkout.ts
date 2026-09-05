import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

import { ProductCheckout } from '../../models/product-checkout.model';
import { CheckoutService } from '../../services/checkout.service';
import { ProductPayload } from '../../interfaces/product-payload.interface';
import { CheckoutPayload } from '../../interfaces/checkout-payload.interface';
import { TokenService } from '../../../../shared/services/token.service';

@Component({
  selector: 'app-checkout',
  imports: [],
  templateUrl: './checkout.html',
  styleUrl: './checkout.scss',
})
export class Checkout {
  router = inject(Router);
  tokenService = inject(TokenService);
  products = this.getProductsFromLocalStorage();
  totalPrice = this.getTotalPrice();
  checkoutService = inject(CheckoutService);

  getTotalPrice() {
    if (!this.products || !this.products.length) {
      return 0;
    }
    return this.products
      ?.map((p) => p.price * p.quantity)
      .reduce((accumulator, currentValue) => accumulator + currentValue);
  }

  addNewOrder() {
    let productsPayload: ProductPayload[] = this.products.map((p): ProductPayload => ({
      productId: p.id,
      quantity: p.quantity,
    }));

    let userToken = this.tokenService.getToken();
    if (userToken === null) {
      this.router.navigate(['/login']);
      return;
    }
    let checkoutPayload: CheckoutPayload = {
      products: productsPayload,
    };

    this.checkoutService.createOrder(checkoutPayload).subscribe(
      () => {
        this.router.navigate(['']);
        this.deleteProductsFromLocalStorage();
      },
      (err) => console.error(err),
    );
  }

  getProductsFromLocalStorage() {
    let keys = Object.keys(localStorage);
    let products: ProductCheckout[] = [];
    for (let i = 0; i < keys.length; i++) {
      if (keys[i] != 'userToken') {
        let item = JSON.parse(localStorage.getItem(keys[i]) as string) as ProductCheckout;
        item.id = keys[i];
        products.push(item);
      }
    }
    return products;
  }
  deleteProductsFromLocalStorage() {
    let keys = Object.keys(localStorage);
    for (let i = 0; i < keys.length; i++) {
      if (keys[i] != 'userToken') {
        localStorage.removeItem(keys[i]);
      }
    }
  }
}
