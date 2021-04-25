using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FakeAtlas.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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
        }

        public static BookingUser Account { get; set; }

        public ICommand InfoCommand { get; set; }

        private void InfoClick() 
        {
            SelectedViewModel = new BookingUserViewModel();
        }

        public ICommand OrdersCommand { get; set; }

        private void OrdersClick()
        {
            SelectedViewModel = new OrdersViewModel();
        }
    }
}
