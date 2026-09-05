import { Component, inject, input, signal} from '@angular/core';
import { ProductCheckout } from '../../../checkout/models/product-checkout.model';

@Component({
  selector: 'app-product-card',
  imports: [],
  templateUrl: './product-card.html',
  styleUrl: './product-card.scss',
  host: {
    class: '',
  },
})
export class ProductCard {
  itemId = input<string>();
  itemName = input<string>();
  itemDescription = input<string>('No description for this item...');
  itemPrice = input<number>();
  itemQuantity = input<number>();

  quantity = signal<number>(0);

  isAdded = signal<boolean>(false);

  ngOnInit() {
    let checkoutProduct: ProductCheckout = JSON.parse(<string>localStorage.getItem(this.itemId() as string));
    this.quantity.set(checkoutProduct?.quantity ?? 0);
    if(this.quantity() > 0){
      this.isAdded.set(true);
    }
    else {
      this.isAdded.set(false);
    }
  }
  btnIncrease() {
    if (this.availableStock()) {
      this.quantity.update(value => value + 1);
      let product: ProductCheckout = {
        id: this.itemId() as string,
        name: this.itemName() as string,
        price: this.itemPrice() as number,
        quantity: this.quantity(),
      };
      localStorage.setItem(
        this.itemId() as string,
        JSON.stringify(product)
      );
    } else {
      console.error('not enough stock');
    }
  }
  btnDecrease() {
    if (this.quantity() > 1) {
      this.quantity.set(this.quantity() - 1);
      let product: ProductCheckout = {
        id: this.itemId() as string,
        name: this.itemName() as string,
        price: this.itemPrice() as number,
        quantity: this.quantity()
      };
      localStorage.setItem(this.itemId() as string,JSON.stringify(product));
    } else {
      localStorage.removeItem(this.itemId() as string);
      this.isAdded.set(false);
      this.quantity.set(0);
    }
  }

  availableStock(): boolean {
    return this.itemQuantity() as number > this.quantity();
  }
}
