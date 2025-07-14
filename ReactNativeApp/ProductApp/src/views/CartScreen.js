import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, Button, ActivityIndicator } from 'react-native';
import { CartViewModel } from '../viewmodels/CartViewModel';

const CartScreen = () => {
  const [viewModel] = useState(() => new CartViewModel());
  const [cartItems, setCartItems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadCart = async () => {
      await viewModel.fetchCart();
      const cart = viewModel.getCart();
      setCartItems(cart?.items || []);
      setLoading(viewModel.getLoading());
      setError(viewModel.getError());
    };
    loadCart();
  }, []);

  const handleRemoveFromCart = async (productId) => {
    await viewModel.removeFromCart(productId, 1);
    const cart = viewModel.getCart();
    setCartItems(cart?.items || []);
    setLoading(viewModel.getLoading());
    setError(viewModel.getError());
  };

  const handleFinalizeCart = async () => {
    await viewModel.finalizeCart();
    const cart = viewModel.getCart();
    setCartItems(cart?.items || []);
    setLoading(viewModel.getLoading());
    setError(viewModel.getError());
  };

  const renderItem = ({ item }) => (
    <View style={{ padding: 10, borderBottomWidth: 1, borderBottomColor: '#ccc' }}>
      <Text>{item.name}</Text>
      <Text>Quantity: {item.quantity}</Text>
      <Text>Price: ${item.price}</Text>
      <Button title="Remove" onPress={() => handleRemoveFromCart(item.itemId)} />
    </View>
  );

  return (
    <View style={{ flex: 1, padding: 10 }}>
      {loading && <ActivityIndicator size="large" color="#0000ff" />}
      {error && <Text style={{ color: 'red' }}>{error}</Text>}
      <FlatList
        data={cartItems}
        renderItem={renderItem}
        keyExtractor={(item) => item.id}
      />
      <Text>Total Price: ${viewModel.getCart()?.totalPrice || 0}</Text>
      <Button title="Finalize Cart" onPress={handleFinalizeCart} />
    </View>
  );
};

export default CartScreen;