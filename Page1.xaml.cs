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
	public partial class Page1 : ContentPage
	{
        public Page1()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private ICommand _addOrder;
        public ICommand AddOrder
        {
            get
            {
                if (_addOrder == null)
                {
                    _addOrder = new Command(AddOrderClicked);
                }
                return _addOrder;
            }
        }

        private async void AddOrderClicked()
        {
            string order = OrderName.Text;
            string note_text = Note.Text;

            if (App.user_name != "Nun")
            {
                if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(note_text))
                {
                    await DisplayAlert("Ошибка", "Заполните все поля", "Ok");
                    return;
                }
                else
                {
                    Notes note = new Notes
                    {
                        user_login = App.user_name,
                        note_name = order,
                        note_text = note_text
                    };

                    App.Db.SaveNote(note);
                    await DisplayAlert("Успех", "Запись добавлена", "Ok");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Шок! Вы не вошли в аккаунт", "Ok");
                return;
            }
        }
    }
}