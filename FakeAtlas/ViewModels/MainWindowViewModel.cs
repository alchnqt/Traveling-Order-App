using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;

namespace FakeAtlas.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static BookingUser _user = new();

        public static BookingUser User { get => _user; set => _user = value; }

        private static UniqueAddress _address = new();

        public static UniqueAddress Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private ViewModelBase _selectedViewModel = new BookingUserViewModel();

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public MainWindowViewModel()
        {
            InfoCommand = new RelayCommand(o => InfoClick());
            OrdersCommand = new RelayCommand(o => OrdersClick());
            HelpCommand = new RelayCommand(o => HelpClick());
        }

        public static BookingUser Account { get; set; }

        public ICommand InfoCommand { get; set; }

        private void InfoClick() => SelectedViewModel = new BookingUserViewModel();

        public ICommand OrdersCommand { get; set; }

        private void OrdersClick() => SelectedViewModel = new OrdersViewModel();

        public ICommand HelpCommand { get; set; }

        private void HelpClick() => SelectedViewModel = new HelpViewModel();

        private ICommand findCommand;
        public ICommand FindCommand => findCommand ??= new DelegateCommand(Find);

        private void Find() => SelectedViewModel = new SearchViewModel();

        private ICommand logOutCommand;
        public ICommand LogOutCommand => logOutCommand ??= new DelegateCommand(LogOut);

        private void LogOut()
        {
            User = null;
        }
    }
}
