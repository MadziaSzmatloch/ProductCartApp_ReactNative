import { v7 as uuidv7 } from 'uuid';

export const CartItem = {
  id: uuidv7(),
  cartId: '',
  itemId: '',
  name: '',
  quantity: 0,
  price: 0,
};