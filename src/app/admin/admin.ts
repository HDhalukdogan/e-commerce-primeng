import { Component, inject, OnInit } from '@angular/core';
import { CategoriesService, CategoryResponse, ProductResponse, ProductsService } from '../core/openapi';
import { ImageModule } from 'primeng/image';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, DialogModule, ButtonModule, TableModule, ImageModule],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin implements OnInit {

  private productsService = inject(ProductsService);
  private categoriesService = inject(CategoriesService);
  products: ProductResponse[] = []
  categories: CategoryResponse[] = [];

  categoryForm: FormGroup;
  productForm: FormGroup;

  displayCategoryModal = false;
  displayProductModal = false;

  categoryImageFile: File | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required]
    });

    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, Validators.required],
      isShow: [true],
      isCarousel: [false],
      image: [null],
      categoryId: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.getProducts();
    this.getCategories();
  }

  getProducts() {
    this.productsService.apiProductsGet().subscribe(products => {
      this.products = products;
    });
  }
  getCategories() {
    this.categoriesService.apiCategoriesGet().subscribe(categories => {
      this.categories = categories;
    });
  }

  openCategoryModal() {
    this.categoryForm.reset();
    this.displayCategoryModal = true;
  }

  openProductModal() {
    this.productForm.reset();
    this.displayProductModal = true;
  }

  onCategoryImageChange(event: any) {
    this.categoryImageFile = event.target.files[0];
  }

  submitCategory() {
    if (this.categoryForm.valid) {
      const formData = new FormData();
      formData.append('name', this.categoryForm.value.name);
      if (this.categoryImageFile) {
        formData.append('image', this.categoryImageFile);
      }
      this.http.post('/api/categories', formData).subscribe(() => {
        this.displayCategoryModal = false;
        this.getCategories();
        this.categoryImageFile = null;
      });
    }
  }

  submitProduct() {
    if (this.productForm.valid) {
      const { name, description, price, isShow, isCarousel, image, categoryId } = this.productForm.value;
      this.productsService.apiProductsPost(
        name,
        description,
        price,
        isShow,
        isCarousel,
        image,
        categoryId
      ).subscribe(() => {
        this.displayProductModal = false;
        this.getProducts();
      });
    }
  }

  onFileChange(event: Event) {
  const input = event.target as HTMLInputElement;
  if (input && input.files && input.files.length > 0) {
    this.productForm.patchValue({ image: input.files[0] });
  }
}
}
