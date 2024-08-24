using App1.Services;
using App1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App1
{
    public partial class App : Application
    {
        private static DB db;
        public static string user_name { get; set; }

        public static string db_path { get; set; }

        public static DB Db
        {
            get
            {
                if(db == null)
                {
                    db_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DB.sqllite3");
                    db = new DB(db_path);
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            NavigationPage.SetHasNavigationBar(this, false);

            user_name = "Nun";
        }

        private NavigationPage CreateMainPage()
        {
            AboutPage aboutPage = new AboutPage();

            NavigationPage navigationPage = new NavigationPage(aboutPage);
            navigationPage.BarBackgroundColor = Color.Green; // установите цвет фона панели навигации, если нужно
            navigationPage.BarTextColor = Color.White; // установите цвет текста панели навигации, если нужно

            NavigationPage.SetHasNavigationBar(aboutPage, false);

            return navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
