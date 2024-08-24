using App1.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using App1.Services;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {    
        private ICommand _onAddClicked;
        public ICommand OnAddClicked
        {
            get
            {
                if (_onAddClicked == null)
                {
                    _onAddClicked = new Command(OnOpenPage1);
                }
                return _onAddClicked;
            }
        }

        private async void OnOpenPage1()
        {
            await NavigateToPage1();
        }

        private async Task NavigateToPage1()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Page1());
        }

        private ICommand _accountClicked;
        public ICommand AccountClicked
        {
            get
            {
                if (_accountClicked == null)
                {
                    _accountClicked = new Command(OnOpenPage3);
                }
                return _accountClicked;
            }
        }

        private async void OnOpenPage3()
        {
            await NavigateToPage3();
        }

        private async Task NavigateToPage3()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Page3());
        }

        
    }
}
