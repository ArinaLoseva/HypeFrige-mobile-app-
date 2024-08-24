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

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page3 : ContentPage
	{
		public Page3 ()
		{
			InitializeComponent ();
            BindingContext = this;
        }

        private ICommand _registrationCommand;
        public ICommand RegistrationCommand
        {
            get
            {
                if (_registrationCommand == null)
                {
                    _registrationCommand = new Command(OnRegistrationClicked);
                }
                return _registrationCommand;
            }
        }

        private async void OnRegistrationClicked()
        {
            string login = Login.Text;
            string password = Password1.Text;
            string second_password = Password2.Text;

            // Проверка на длину и допустимые символы для логина
            if (string.IsNullOrEmpty(login) || login.Length < 4 || !IsValidLoginOrPassword(login))
            {
                await DisplayAlert("Ошибка", "Логин должен быть не менее 4 символов и содержать только латинские буквы, цифры или специальные знаки.", "Ok");
                return;
            }

            if (password == second_password)
            {
                // Проверка на длину и допустимые символы для пароля
                if (string.IsNullOrEmpty(password) || password.Length < 8 || !IsValidLoginOrPassword(password))
                {
                    await DisplayAlert("Ошибка", "Пароль должен быть не менее 8 символов и содержать латинские буквы, цифры и специальные знаки.", "Ok");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный повторный пароль", "Ok");
                return;
            }

            Users user = new Users
            {
                user_login = login,
                user_password = password
            };

            App.Db.SaveUser(user);
            App.user_name = login;

            await DisplayAlert("Успех", "Учетная запись успешно создана", "Ok");
            Login.Text = "";
            Password1.Text = "";
            Password2.Text = "";
        }

        private bool IsValidLoginOrPassword(string input)
        {
            // Проверка, что строка содержит только латинские буквы, цифры и специальные знаки
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};:""\\|,.<>\/?]*$");
        }

        private ICommand _authorizationCommand;
        public ICommand AuthorizationCommand
        {
            get
            {
                if (_authorizationCommand == null)
                {
                    _authorizationCommand = new Command(OnAuthorizationClicked);
                }
                return _authorizationCommand;
            }
        }

        private async void OnAuthorizationClicked()
        {
            string login = Login.Text;
            string password = Password1.Text;

            // Предполагаем, что у вас есть класс DBHelper, в котором вы инициализировали подключение к базе данных
            var db = new SQLiteAsyncConnection(App.db_path);
            await db.CreateTableAsync<Users>();

            var user = await db.Table<Users>().FirstOrDefaultAsync(u => u.user_login == login && u.user_password == password);

            if (user != null)
            {
                // Если находим пользователя, выводим сообщение об успешной авторизации
                await DisplayAlert("Успех", "Вы успешно вошли в учетную запись", "Ok");
                App.user_name = login;
                // Очищаем поля ввода
                Login.Text = "";
                Password1.Text = "";
                Password2.Text = "";
            }
            else
            {
                // Если нет - сообщаем об ошибке
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ok");
            }
        }
    }
}