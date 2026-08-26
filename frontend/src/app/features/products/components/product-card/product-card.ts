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
  cartQuantity = input<number>();

  isAdded = signal<boolean>(false);

  displayQuantityNumber = viewChild.required<ElementRef<HTMLInputElement>>('displayQuantityNumber');

  // ngOnInit() {
  //   if(this.cartQuantity !== undefined){
  //     this.isAdded.set(true);
  //     this.displayQuantityNumber().nativeElement.valueAsNumber = this.cartQuantity() as number;
  //   }
  // }
  btnIncrease() {
    let value = this.displayQuantityNumber().nativeElement.valueAsNumber + 1;
    let isAvailable = this.availableStock(this.itemId() as string, value);
    if (isAvailable) {
      this.displayQuantityNumber().nativeElement.valueAsNumber += 1;
      localStorage.setItem(
        this.itemId() as string,
        this.displayQuantityNumber().nativeElement.valueAsNumber.toString(),
      );
    } else {
      console.error('not enough stock');
    }
  }
  btnDecrease() {
    let value = this.displayQuantityNumber().nativeElement.valueAsNumber;
    if (value > 1) {
      this.displayQuantityNumber().nativeElement.valueAsNumber -= 1;
      localStorage.setItem(
        this.itemId() as string,
        this.displayQuantityNumber().nativeElement.valueAsNumber.toString(),
      );
    } else {
      localStorage.removeItem(this.itemId() as string);
      this.isAdded.set(false);
    }
  }

  quantity = 1;
  quantitySignal = signal<number>(0);
  availableStock(productId: string, currentQuantity: number): boolean {

    this.productService.addProductItemToLocalStorage(productId).subscribe(
      (value) => {
        this.quantity = value;
      },
      (error) =>{
        console.error(error)
        return false;
      },
    );
    return currentQuantity <= this.quantity;
  }

  changeToStepper() {
    if (this.availableStock(this.itemId() as string, 1)) {
      this.isAdded.set(true);
      localStorage.setItem(
        this.itemId() as string,
        '1'
      );
    } else {
      console.error('not enough stock');
    }
  }
}
