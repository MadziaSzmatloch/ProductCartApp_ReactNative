import axios from 'axios';

const PRODUCT_API_BASE_URL = 'http://127.0.0.1:8080/products-api/product';
const CART_API_BASE_URL = 'http://127.0.0.1:8080/cart-api/cart';

export class ApiService {
  static async getProducts() {
    try {
      const response = await axios.get(`${PRODUCT_API_BASE_URL}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching products:', error);
      throw error;
    }
  }

  static async createCart(userId) {
    try {
      const response = await axios.post(`${CART_API_BASE_URL}`, { userId });
      return response.data;
    } catch (error) {
      console.error('Error creating cart:', error);
      throw error;
    }
  }

  static async getCart(cartId) {
    try {
      const response = await axios.get(`${CART_API_BASE_URL}/${cartId}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching cart:', error);
      throw error;
    }
  }

  static async addToCart({ cartId, productId, quantity }) {
    try {
      const response = await axios.post(`${CART_API_BASE_URL}/addProduct`, {
        cartId,
        productId,
        quantity,
      });
      return response.data;
    } catch (error) {
      console.error('Error adding to cart:', error);
      throw error;
    }
  }

  static async removeFromCart({ cartId, productId, quantity }) {
    try {
      const response = await axios.post(`${CART_API_BASE_URL}/deleteProduct`, {
        cartId,
        productId,
        quantity,
      });
      return response.data;
    } catch (error) {
      console.error('Error removing from cart:', error);
      throw error;
    }
  }

  static async finalizeCart(cartId) {
    try {
      const response = await axios.post(`${CART_API_BASE_URL}/finalizeCart`, { cartId });
      return response.data;
    } catch (error) {
      console.error('Error finalizing cart:', error);
      throw error;
    }
  }

}