import { Component, inject, OnInit } from '@angular/core';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, CarouselModule, ButtonModule, DialogModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  private http = inject(HttpClient);

  carouselImages: string[] = [];
  products: any[] = [];

  displayModal = false;
  selectedProduct: any = null;

  ngOnInit(): void {
    this.http.get<any>('data/products.json').subscribe(data => {
      this.carouselImages = data.carouselImages;
      this.products = data.products;
    });
  }

  showProduct(product: any) {
    this.selectedProduct = product;
    this.displayModal = true;
  }
}
