using FakeAtlas.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using FakeAtlas.Context.UnitOfWork;
using System.Text.RegularExpressions;
using FakeAtlas.ViewModels.Management;
using FakeAtlas.FakeAtlasUIComponents;

namespace FakeAtlas.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        #region field regex
        public static Regex CostRegex = new(@"^\d{1, }$");
        public static Regex TimeRegex = new(@"^(\d{4})\\(\d{2})\\(\d{2}) (\d{2}):(\d{2}):(\d{2})&");
        public static Regex DigitalRegex = new(@"^\d$");
        #endregion

        private ObservableCollection<AvailableOrder> _selectedOrders = new();

        public ObservableCollection<AvailableOrder> SelectedOrders
        {
            get { return _selectedOrders; }
            set
            {
                _selectedOrders = value;
                OnPropertyChanged(nameof(SelectedOrders));
            }
        }

        private ObservableCollection<Shipper> _selectedShippers = new();

        public ObservableCollection<Shipper> SelectedShippers
        {
            get { return _selectedShippers; }
            set
            {
                _selectedShippers = value;
                OnPropertyChanged(nameof(SelectedShippers));
            }
        }

        private AvailableOrder _selectedOrder = new();

        public AvailableOrder SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        private Shipper _selectedShipper = new();

        public Shipper SelectedShipper
        {
            get { return _selectedShipper; }
            set
            {
                _selectedShipper = value;
                OnPropertyChanged(nameof(SelectedShipper));
            }
        }

        private string _companyName = "";

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public AdminViewModel()
        {
            using (UnitOfWork unit = new())
            {
                SelectedOrders = new ObservableCollection<AvailableOrder>(from item in unit.AvailableOrdersRepository.GetWithInclude(o => o.Shipper) select item);
                SelectedShippers = new ObservableCollection<Shipper>(from item in unit.ShipperRepository.GetWithInclude(o => o.AvailableOrders) select item);
            }
        }

        private ICommand saveCompanyCommand;
        public ICommand SaveCompanyCommand => saveCompanyCommand ??= new DelegateCommand(SaveCompany);

        private void SaveCompany()
        {
            using (UnitOfWork unit = new())
            {
                try
                {
                    unit.ShipperRepository.Create(SelectedShipper);
                    unit.Save();
                    SelectedShippers = new ObservableCollection<Shipper>(from item in unit.ShipperRepository.GetWithInclude(o => o.AvailableOrders) select item);
                    SelectedOrder = new();
                    SelectedShipper = new();
                }
                catch (Exception e)
                {
                    FakeAtlasMessageBoxService box = new();
                    box.ShowMessage(e.Message);
                }
            }
        }

        private ICommand saveRouteCommand;
        public ICommand SaveRouteCommand => saveRouteCommand ??= new DelegateCommand(SaveRoute);

        private void SaveRoute()
        {
            try
            {
                using (UnitOfWork unit = new())
                {
                    SelectedOrder.ShipperId = (from order in unit.ShipperRepository.Get() 
                                               where order.FullName.Equals(CompanyName) select order).SingleOrDefault().Id;
                    unit.AvailableOrdersRepository.Create(SelectedOrder);
                    unit.Save();
                    SelectedOrders = new ObservableCollection<AvailableOrder>(from item in unit.AvailableOrdersRepository.GetWithInclude(o => o.Shipper) select item);
                    SelectedOrder = new();
                    SelectedShipper = new();
                }
            }
            catch (Exception e)
            {
                FakeAtlasMessageBoxService box = new();
                box.ShowMessage(e.Message);
            }
        }

        private ICommand removeOrderCommand;
        public ICommand RemoveOrderCommand => removeOrderCommand ??= new DelegateCommand(RemoveOrder);

        private void RemoveOrder()
        {
            try
            {
                using (UnitOfWork unit = new())
                {
                    unit.AvailableOrdersRepository.Remove(SelectedOrder);
                    unit.Save();
                    SelectedOrders = new ObservableCollection<AvailableOrder>(from item in unit.AvailableOrdersRepository.GetWithInclude(o => o.Shipper) select item);
                    SelectedOrder = new();
                    SelectedShipper = new();
                }
            }
            catch (Exception e)
            {
                FakeAtlasMessageBoxService box = new();
                box.ShowMessage(FakeAtlasMessageBox.MessageType.DeleteOrderError, CurrentLocalization);
            }
        }

        private ICommand removeCompanyCommand;
        public ICommand RemoveCompanyCommand => removeCompanyCommand ??= new DelegateCommand(RemoveCompany);

        private void RemoveCompany()
        {
            try
            {
                using (UnitOfWork unit = new())
                {
                    unit.ShipperRepository.Remove(SelectedShipper);
                    unit.Save();
                    SelectedOrders = new ObservableCollection<AvailableOrder>(from item in unit.AvailableOrdersRepository.GetWithInclude(o => o.Shipper) select item);
                    SelectedShippers = new ObservableCollection<Shipper>(from item in unit.ShipperRepository.GetWithInclude(o => o.AvailableOrders) select item);
                    SelectedOrder = new();
                    SelectedShipper = new();
                }
            }
            catch (Exception e)
            {
                FakeAtlasMessageBoxService box = new();
                box.ShowMessage(FakeAtlasMessageBox.MessageType.DeleteCompanyError, CurrentLocalization);
            }
        }
    }
}
