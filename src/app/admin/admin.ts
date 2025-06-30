import { Component, inject, OnInit } from '@angular/core';
import { ProductResponse, ProductsService } from '../core/openapi';
import { ImageModule } from 'primeng/image';

@Component({
  selector: 'app-admin',
  imports: [ImageModule],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin implements OnInit  {

  private productService = inject(ProductsService);
  products : ProductResponse[] = []
   
  ngOnInit() {
    this.productService.apiProductsGet().subscribe(products => {
      this.products = products;
    });
  }
}
