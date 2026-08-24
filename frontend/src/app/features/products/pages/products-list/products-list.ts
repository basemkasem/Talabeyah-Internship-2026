import { Component, inject, signal } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../interfaces/product.interface';
import { ProductCard } from "../../components/product-card/product-card";

@Component({
  selector: 'app-products-list',
  imports: [ProductCard],
  templateUrl: './products-list.html',
  styleUrl: './products-list.scss',
})

export class ProductsList {
  productService = inject(ProductService);
  products = signal<Product[]>([]);
  ngOnInit() {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts(1, 10).subscribe(
      (products) => {
        this.products.set(products);
      },
      (error) => {
        console.error(error);
      },
    );
  }
}

