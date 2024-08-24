using App1.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
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