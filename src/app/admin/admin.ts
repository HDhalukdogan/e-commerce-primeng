import { Component, inject, OnInit } from '@angular/core';
import { CategoriesService, CategoryResponse, ProductResponse, ProductsService } from '../core/openapi';
import { ImageModule } from 'primeng/image';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

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


  editCategoryData: CategoryResponse | null = null;
  editProductData: ProductResponse | null = null;

  constructor(private fb: FormBuilder) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      image: [null],
    });

    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, Validators.required],
      isShow: [true],
      isCarousel: [false],
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
    this.editCategoryData = null;
    this.displayCategoryModal = true;
  }

  editCategory(cat: CategoryResponse) {
    this.categoryForm.patchValue({ name: cat.name, description: cat.description });
    this.editCategoryData = cat;
    this.displayCategoryModal = true;
  }

  submitCategory() {
    if (this.categoryForm.valid) {
      const { name, description, image } = this.categoryForm.value;
      if (this.editCategoryData) {
        this.categoriesService.apiCategoriesPut(this.editCategoryData.id, name, description, image).subscribe(() => {
          this.displayCategoryModal = false;
          this.getCategories();
          this.editCategoryData = null;
        });
      } else {
        this.categoriesService.apiCategoriesPost(name, description, image).subscribe(() => {
          this.displayCategoryModal = false;
          this.getCategories();
        });
      }
    }
  }

  openProductModal() {
    this.productForm.reset({
      name: '',
      description: '',
      price: 0,
      isShow: true,
      isCarousel: false,
      categoryId: null
    });
    this.editProductData = null;
    this.displayProductModal = true;
  }

  editProduct(prod: ProductResponse) {
    this.productForm.patchValue({
      name: prod.name,
      description: prod.description,
      price: prod.price,
      isShow: prod.isShow,
      isCarousel: prod.isCarousel,
      categoryId: prod.categoryId
    });
    this.editProductData = prod;
    this.displayProductModal = true;
  }

  submitProduct() {
    if (this.productForm.valid) {
      if (this.editProductData) {
        this.productsService.apiProductsPut(
          { ...this.productForm.value, id: this.editProductData.id }
        ).subscribe(() => {
          this.displayProductModal = false;
          this.getProducts();
          this.editProductData = null;
        });
      } else {
        this.productsService.apiProductsPost(
          this.productForm.value
        ).subscribe(() => {
          this.displayProductModal = false;
          this.getProducts();
        });
      }
    }
  }

  onCategoryImageChange(event: any) {
    const input = event.target as HTMLInputElement;
    if (input && input.files && input.files.length > 0) {
      this.categoryForm.patchValue({ image: input.files[0] });
    }
  }

  deleteCategory(category: CategoryResponse) {
    if (category.id === undefined) {
      alert('Category ID is missing. Cannot delete this category.');
      return;
    }
    if (confirm(`Are you sure you want to delete category "${category.name}"?`)) {
      this.categoriesService.apiCategoriesIdDelete(category.id).subscribe(() => {
        this.getCategories();
      });
    }
  }

  deleteProduct(product: ProductResponse) {
    if (product.id === undefined) {
      alert('Product ID is missing. Cannot delete this product.');
      return;
    }
    if (confirm(`Are you sure you want to delete product "${product.name}"?`)) {
      this.productsService.apiProductsIdDelete(product.id).subscribe(() => {
        this.getProducts();
      });
    }
  }


  showImagesModal = false;
  selectedProductForImages: ProductResponse | null = null;

  showImages(product: ProductResponse) {
    this.selectedProductForImages = product;
    this.showImagesModal = true;
  }

  productImageFile: File | null = null;

  onProductImageChange(event: any) {
    const input = event.target as HTMLInputElement;
    if (input && input.files && input.files.length > 0) {
      this.productImageFile = input.files[0];
    }
  }

  uploadProductImage() {
    if (this.selectedProductForImages && this.productImageFile) {
      this.productsService.apiProductsIdImagesPost(this.selectedProductForImages.id!, this.productImageFile)
        .subscribe(() => {
          this.getProducts();
          this.showImagesModal = false;
          this.selectedProductForImages = null;
          this.productImageFile = null;
        });
    }
  }
}
