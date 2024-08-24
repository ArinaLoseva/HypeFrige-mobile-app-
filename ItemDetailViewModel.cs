using App1.Models;
using App1.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        //вход в аккаунт
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

        //фильтрация продуктов
        private bool _isFilterMenuVisible;
        public bool IsFilterMenuVisible
        {
            get => _isFilterMenuVisible;
            set
            {
                SetProperty(ref _isFilterMenuVisible, value);
                OnPropertyChanged(nameof(IsFilterMenuVisible));
            }
        }

        private ICommand _toggleFilterCommand;
        public ICommand ToggleFilterCommand
        {
            get
            {
                if (_toggleFilterCommand == null)
                {
                    _toggleFilterCommand = new Command(() => IsFilterMenuVisible = !IsFilterMenuVisible);
                }
                return _toggleFilterCommand;
            }
        }

        private ICommand _filterByCommand;
        public ICommand FilterByCommand
        {
            get
            {
                if (_filterByCommand == null)
                {
                    _filterByCommand = new Command<string>(FilterBy);
                }
                return _filterByCommand;
            }
        }

        private void FilterBy(string filterOption)
        {
            // Implement your filter logic here
            Console.WriteLine($"Filtering by: {filterOption}");
        }
    }
}
