using App1.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
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
            await LoadNotesAsync();
        }

        //dinamic 

        public async Task LoadNotesAsync()
        {
            //string username = "user_name"; // Пример пользователя, замените на актуальное значение
            //DB dB = new DB();
            var notes = await GetNotesAsync(App.user_name);
            StackLayout notesStack = new StackLayout { Orientation = StackOrientation.Vertical };

            foreach (var note in notes)
            {
                Frame frame = new Frame
                {
                    BackgroundColor = Color.FromHex("#EBF3E1"),
                    Margin = new Thickness(0, 2),
                    CornerRadius = 25,
                    HeightRequest = 20
                };

                StackLayout horizontalLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

                Image checkboxImage = new Image
                {
                    Source = "https://i.ibb.co/5xxK75y/uncheck.png",
                    WidthRequest = 23
                };
                // Добавьте здесь привязку для Command

                StackLayout verticalLayout = new StackLayout { Orientation = StackOrientation.Vertical, Margin = new Thickness(10, -12) };

                Label nameLabel = new Label
                {
                    Text = note.note_name,
                    TextColor = Color.Black
                };

                Label textLabel = new Label
                {
                    Text = note.note_text,
                    TextColor = Color.FromHex("#5A5C59")
                };

                verticalLayout.Children.Add(nameLabel);
                verticalLayout.Children.Add(textLabel);

                horizontalLayout.Children.Add(checkboxImage);
                horizontalLayout.Children.Add(verticalLayout);
                frame.Content = horizontalLayout;

                notesStack.Children.Add(frame);
            }

            notesScrollView.Content = notesStack;
        }

        public Task<List<Notes>> GetNotesAsync(string username)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqllite3");
            ISQLiteConnection conn = new SQLiteConnection(path);
            return Task.Run(() =>
                conn.Table<Notes>().Where(n => n.user_login == username).ToList()
            );
        }
    }
}