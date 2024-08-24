using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using SQLite;
using System.IO;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{
		public Page2 ()
		{
			InitializeComponent ();
            BindingContext = this;
        }

        private ICommand _addPurchase;
        public ICommand AddPurchase
        {
            get
            {
                if (_addPurchase == null)
                {
                    _addPurchase = new Command(AddPurchaseClicked);
                }
                return _addPurchase;
            }
        }

        private async void AddPurchaseClicked()
        {
            string purchase = Purchase.Text;

            // Получение даты
            Xamarin.Forms.DatePicker myDatePicker = this.FindByName<Xamarin.Forms.DatePicker>("myDatePicker");
            DateTime selectedDate = myDatePicker.Date;
            string date = selectedDate.ToString("dd.MM.yyyy");

            // Получение выбранного типа продукта
            Xamarin.Forms.Picker picker_type = this.FindByName<Xamarin.Forms.Picker>("picker_type");
            string type = picker_type.SelectedItem?.ToString();

            // Получение выбранного места хранения
            Xamarin.Forms.Picker picker_place = this.FindByName<Xamarin.Forms.Picker>("picker_place");
            string place = picker_place.SelectedItem?.ToString();

            if (App.user_name != "Nun")
            {
                if (string.IsNullOrEmpty(purchase) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(place))
                {
                    await DisplayAlert("Ошибка", "Заполните все поля", "Ok");
                    return;
                }
                else
                {
                    if (IsOnlyRussian(purchase))
                    {
                        Products product = new Products
                        {
                            product_name = purchase,
                            product_date = date,
                            product_type = type,
                            product_place = place,
                            user_login = App.user_name
                        };

                        App.Db.SaveProduct(product);
                        await DisplayAlert("Успех", "Ваш заказ добавлен в " + place , "Ok");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Название продукта должно быть на русском языке", "Ok");
                        return;
                    }
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Шок! Вы не вошли в аккаунт", "Ok");
                return;
            }
        }

        private bool IsOnlyRussian(string input)
        {
            // Проверка, что строка содержит только русские буквы
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[а-яА-ЯёЁ\s]+$");
        }
    }
}
