import { Component, ElementRef, inject, input, signal, viewChild } from '@angular/core';
import { ProductService } from '../../services/product.service';

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
  productService = inject(ProductService);
  itemId = input<string>();
  itemName = input<string>();
  itemDescription = input<string>('No description for this item...');
  itemPrice = input<number>();
  itemQuantity = input<number>();

  quantity = signal<number>(0);

  isAdded = signal<boolean>(false);

  ngOnInit() {
    this.quantity.set(Number(localStorage.getItem(this.itemId() as string)) || 0);
    if(this.quantity() > 0){
      this.isAdded.set(true);
    }
  }
  btnIncrease() {
    if (this.availableStock()) {
      this.quantity.set(this.quantity() + 1);
      localStorage.setItem(
        this.itemId() as string,
        this.quantity().toString()
      );
    } else {
      console.error('not enough stock');
    }
  }
  btnDecrease() {
    if (this.quantity() > 1) {
      this.quantity.set(this.quantity() - 1);
      localStorage.setItem(this.itemId() as string, this.quantity().toString());
    } else {
      localStorage.removeItem(this.itemId() as string);
      this.isAdded.set(false);
      this.quantity.set(0);
    }
  }

  availableStock(): boolean {
    return this.itemQuantity() as number >= this.quantity();
  }
}
