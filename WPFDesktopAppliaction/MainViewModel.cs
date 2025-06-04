using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDesktopAppliaction
{
    public class MainViewModel : ObservableObject
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Product> Products { get; } = new();

        public MainViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoadProductsCommand = new AsyncRelayCommand(LoadProductsAsync);
        }

        public IAsyncRelayCommand LoadProductsCommand { get; }

        private async Task LoadProductsAsync()
        {
            var products = await _apiService.GetProductsAsync();
            Products.Clear();
            foreach (var p in products)
                Products.Add(p);
        }
    }

}
