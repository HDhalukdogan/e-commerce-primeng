import { Component, inject, OnInit } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { HomePageResponse, HomeProduct, ProductImageResponse, ProductResponse, ProductsService } from '../core/openapi';
import { ImageModule } from 'primeng/image';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, CarouselModule, ButtonModule, DialogModule, ImageModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  private productsService = inject(ProductsService);

  homePageResponse: HomePageResponse = {
    carouselImages: [],
    homeProducts: []
  } ;

  displayModal = false;
  selectedProduct: HomeProduct | null = null;

  ngOnInit(): void {
    this.loadHomepage();
  }


  loadHomepage() {
    this.productsService.apiProductsHomePageGet().subscribe(response => {
      this.homePageResponse = response;
    });
  }

  showProduct(product: HomeProduct) {
    this.selectedProduct = product;
    this.displayModal = true;
  }


}
