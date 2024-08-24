using App1.Models;
using App1.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App1.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (App.user_name != "Nun")
            {
                await InitializeAsync();
            }
        }

        private async Task InitializeAsync()
        {
            await LoadProductsAsync();
        }

        //dinamic 

        public async Task LoadProductsAsync()
        {
            try
            {
                fridgeStackLayout.Children.Clear();
                freezerStackLayout.Children.Clear();
                pantryStackLayout.Children.Clear();

                var products = await GetProductsAsync(App.user_name);

                foreach (var product in products)
                {
                    StackLayout productLayout = new StackLayout
                    {
                        Padding = new Thickness(0),
                        Margin = new Thickness(0),
                        WidthRequest = 330,
                        HeightRequest = 30,
                        Orientation = StackOrientation.Vertical
                    };

                    StackLayout horizontalLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };

                    Image productImage = new Image
                    {
                        Source = "https://i.ibb.co/vLsyrsP/food-icon.png",
                        WidthRequest = 21,
                        HeightRequest = 21
                    };

                    Label foodNameLabel = new Label
                    {
                        Text = product.product_name,
                        TextColor = Color.FromHex("#5A5C59"),
                        FontSize = 14,
                        FontFamily = "Jost",
                        Margin = new Thickness(5)
                    };

                    Label foodDateLabel = new Label
                    {
                        Text = product. product_date.ToString(),
                        TextColor = Color.FromHex("#5A5C59"),
                        FontSize = 14,
                        FontFamily = "Jost",
                        Margin = new Thickness(5),
                        HorizontalOptions = LayoutOptions.EndAndExpand
                    };

                    BoxView separator = new BoxView
                    {
                        Color = Color.FromHex("#EDEDED"),
                        WidthRequest = 330,
                        HeightRequest = 1
                    };

                    horizontalLayout.Children.Add(productImage);
                    horizontalLayout.Children.Add(foodNameLabel);
                    horizontalLayout.Children.Add(foodDateLabel);

                    productLayout.Children.Add(horizontalLayout);
                    productLayout.Children.Add(separator);

                    // Add the productLayout to the appropriate StackLayout based on product_place
                    if (product.product_place == "Холодильник")
                    {
                        fridgeStackLayout.Children.Add(productLayout);
                    }
                    else if (product.product_place == "Морозильная камера")
                    {
                        freezerStackLayout.Children.Add(productLayout);
                    }
                    else if (product.product_place == "Кладовая")
                    {
                        pantryStackLayout.Children.Add(productLayout);
                    }
                    else
                    {
                        fridgeStackLayout.Children.Add(productLayout);
                        //await DisplayAlert("Ошибка", "ошибка в бэдэ", "Ok");
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        public Task<List<Products>> GetProductsAsync(string username)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqllite3");
            var conn = new SQLiteConnection(path);
            return Task.Run(() =>
                conn.Table<Products>().Where(n => n.user_login == username).ToList()
            );
        }
    }
}