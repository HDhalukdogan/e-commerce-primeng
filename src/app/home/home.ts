import { Component, inject, OnInit } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ProductImageResponse, ProductResponse, ProductsService } from '../core/openapi';
import { ImageModule } from 'primeng/image';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, CarouselModule, ButtonModule, DialogModule, ImageModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  private http = inject(HttpClient);
  private productsService = inject(ProductsService);

  carouselImages: string[] = [];
  products: any[] = [];
  productResponses: ProductResponse[] = [];
  carouselProductImages: ProductImageResponse[] = [];

  displayModal = false;
  selectedProduct: any = null;

  ngOnInit(): void {
    this.loadProducts();
    this.http.get<any>('data/products.json').subscribe(data => {
      this.carouselImages = data.carouselImages;
      this.products = data.products;
    });
  }


  loadProducts() {
    this.productsService.apiProductsGet().subscribe(products => {
      this.productResponses = products;
      this.carouselProductImages = products
        .filter(product => product.isCarousel)
        .flatMap(product => product.productImages?.filter(image => image.isCover) ?? []);
    });
  }

  showProduct(product: any) {
    this.selectedProduct = product;
    this.displayModal = true;
  }


}
