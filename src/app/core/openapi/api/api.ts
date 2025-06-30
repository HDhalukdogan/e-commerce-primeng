export * from './categories.service';
import { CategoriesService } from './categories.service';
export * from './demoEntities.service';
import { DemoEntitiesService } from './demoEntities.service';
export * from './products.service';
import { ProductsService } from './products.service';
export const APIS = [CategoriesService, DemoEntitiesService, ProductsService];
