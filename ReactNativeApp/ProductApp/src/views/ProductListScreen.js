import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, TouchableOpacity, ActivityIndicator } from 'react-native';
import { useNavigation, useFocusEffect } from '@react-navigation/native';
import { ProductViewModel } from '../viewmodels/ProductViewModel';

const ProductListScreen = () => {
  const navigation = useNavigation();
  const [viewModel] = useState(() => new ProductViewModel());
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);


  useFocusEffect(
    React.useCallback(() => {
      const loadProducts = async () => {
        setLoading(true);
        await viewModel.fetchProducts();
        setProducts(viewModel.getProducts());
        setLoading(viewModel.getLoading());
        setError(viewModel.getError());
      };
      loadProducts();
    }, [viewModel]) // viewModel jako zależność, aby uniknąć błędów
  );


  const renderItem = ({ item }) => (
    <TouchableOpacity
      onPress={() => navigation.navigate('ProductDetail', { product: item })}
      style={{ padding: 10, borderBottomWidth: 1, borderBottomColor: '#ccc' }}
    >
      <Text style={{ fontSize: 18 }}>{item.name}</Text>
      <Text>Price: ${item.price}</Text>
      <Text>Quantity: {item.quantity}</Text>
    </TouchableOpacity>
  );

  return (
    <View style={{ flex: 1, padding: 10 }}>
      {loading && <ActivityIndicator size="large" color="#0000ff" />}
      {error && <Text style={{ color: 'red' }}>{error}</Text>}
      <FlatList
        data={products}
        renderItem={renderItem}
        keyExtractor={(item) => item.id}
      />
      <TouchableOpacity
        onPress={() => navigation.navigate('Cart')}
        style={{ padding: 10, backgroundColor: '#007bff', marginTop: 10 }}
      >
        <Text style={{ color: '#fff', textAlign: 'center' }}>Go to Cart</Text>
      </TouchableOpacity>
    </View>
  );
};

export default ProductListScreen;