import { ApiService } from '../services/ApiService';
import { StorageService } from '../services/StorageService';

export class ProductViewModel {
  constructor() {
    this.products = [];
    this.loading = false;
    this.error = null;
  }

  getProducts() {
    return this.products;
  }

  getLoading() {
    return this.loading;
  }

  getError() {
    return this.error;
  }

  async fetchProducts() {
    this.loading = true;
    try {
      const products = await ApiService.getProducts();
      this.products = products;
      await StorageService.saveProducts(products);
      this.error = null;
    } catch (error) {
      this.error = 'Failed to fetch products';
      const localProducts = await StorageService.getProducts();
      if (localProducts) {
        this.products = localProducts;
        this.error = null;
      }
    } finally {
      this.loading = false;
    }
  }
}