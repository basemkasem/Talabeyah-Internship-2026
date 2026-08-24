import { Component, input } from '@angular/core';

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
  itemName = input<string>();
  itemDescription = input<string>();
  itemPrice = input<number>();
}
