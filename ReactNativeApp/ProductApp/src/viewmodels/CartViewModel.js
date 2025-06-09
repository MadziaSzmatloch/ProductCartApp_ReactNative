import { ApiService } from '../services/ApiService';
import { StorageService } from '../services/StorageService';

let instance = null;

export class CartViewModel {
  constructor() {
    if (instance) return instance;
    this.cart = null;
    this.loading = false;
    this.error = null;
    this.userId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
    this.cartId = null;
    instance = this;
  }

  getCart() {
    return this.cart;
  }

  getLoading() {
    return this.loading;
  }

  getError() {
    return this.error;
  }

  getCartId() {
    return this.cartId;
  }

  async initializeCartId() {
      console.log('initialize');
    const savedCartId = await StorageService.getCartId();
    if (savedCartId) {
      this.cartId = savedCartId;
    } else {
      await this.createCart();
    }
  }

  async createCart() {
      console.log('create');
    this.loading = true;
    try {
      const cart = await ApiService.createCart(this.userId);
      this.cart = cart;
      this.cartId = cart.id;
      await StorageService.saveCartId(this.cartId);
      this.error = null;
    } catch (error) {
      this.error = 'Failed to create cart';
    } finally {
      this.loading = false;
    }
  }

  async fetchCart() {
    if (!this.cartId) {
      await this.initializeCartId();
    }
    this.loading = true;
    try {
      this.cart = await ApiService.getCart(this.cartId);
      this.error = null;
    } catch (error) {
      this.error = 'Failed to fetch cart';
    } finally {
      this.loading = false;
    }
  }

  async addToCart(productId, quantity) {
    if (!this.cartId) {
      await this.initializeCartId();
    }
    this.loading = true;
    try {
      console.log('dodawanie');
      await ApiService.addToCart({
        cartId: this.cartId,
        productId,
        quantity,
      });
      await this.fetchCart();
      this.error = null;
    } catch (error) {
      this.error = 'Failed to add item to cart';
    } finally {
      this.loading = false;
    }
  }

  async removeFromCart(productId, quantity) {
    if (!this.cartId) {
      this.error = 'No cart exists';
      return;
    }
    this.loading = true;
    try {
      await ApiService.removeFromCart({
        cartId: this.cartId,
        productId,
        quantity,
      });
      await this.fetchCart();
      this.error = null;
    } catch (error) {
      this.error = 'Failed to remove item from cart';
    } finally {
      this.loading = false;
    }
  }

  async finalizeCart() {
    if (!this.cartId) {
      this.error = 'No cart exists';
      return;
    }
    this.loading = true;
    try {
      await ApiService.finalizeCart(this.cartId);
      this.cart = null;
      this.cartId = null;
      await StorageService.saveCartId(null);
      this.error = null;
    } catch (error) {
      this.error = 'Failed to finalize cart';
    } finally {
      this.loading = false;
    }
  }
}