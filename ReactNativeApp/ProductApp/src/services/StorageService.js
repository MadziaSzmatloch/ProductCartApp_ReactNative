import AsyncStorage from '@react-native-async-storage/async-storage';

export class StorageService {
  static PRODUCTS_KEY = 'products';
  static CART_ID_KEY = 'cartId';

  static async saveProducts(products) {
    try {
      await AsyncStorage.setItem(this.PRODUCTS_KEY, JSON.stringify(products));
    } catch (error) {
      console.error('Error saving products:', error);
    }
  }

  static async getProducts() {
    try {
      const json = await AsyncStorage.getItem(this.PRODUCTS_KEY);
      return json ? JSON.parse(json) : null;
    } catch (error) {
      console.error('Error retrieving products:', error);
      return null;
    }
  }

  static async saveCartId(cartId) {
    try {
      if (cartId) {
        await AsyncStorage.setItem(this.CART_ID_KEY, cartId);
      } else {
        await AsyncStorage.removeItem(this.CART_ID_KEY);
      }
    } catch (error) {
      console.error('Error saving cartId:', error);
    }
  }

  static async getCartId() {
    try {
      const cartId = await AsyncStorage.getItem(this.CART_ID_KEY);
      return cartId || null;
    } catch (error) {
      console.error('Error retrieving cartId:', error);
      return null;
    }
  }
}