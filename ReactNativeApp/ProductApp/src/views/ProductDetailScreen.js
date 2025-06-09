import React, { useState } from 'react';
import { View, Text, Button, ActivityIndicator } from 'react-native';
import { useRoute } from '@react-navigation/native';
import { CartViewModel } from '../viewmodels/CartViewModel';

const ProductDetailScreen = () => {
  const route = useRoute();
  const { product } = route.params;
  const [viewModel] = useState(() => new CartViewModel());
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleAddToCart = async () => {
    setLoading(true);
    await viewModel.addToCart(product.id, 1); // Default quantity: 1
    setLoading(viewModel.getLoading());
    setError(viewModel.getError());
  };

  return (
    <View style={{ flex: 1, padding: 10 }}>
      <Text style={{ fontSize: 24, fontWeight: 'bold' }}>{product.name}</Text>
      <Text>Price: ${product.price}</Text>
      <Text>Quantity: {product.quantity}</Text>
      {loading && <ActivityIndicator size="large" color="#0000ff" />}
      {error && <Text style={{ color: 'red' }}>{error}</Text>}
      <Button title="Add to Cart" onPress={handleAddToCart} />
    </View>
  );
};

export default ProductDetailScreen;