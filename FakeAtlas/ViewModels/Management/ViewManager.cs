using FakeAtlas.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FakeAtlas.ViewModels.Management
{
    public class ViewManager
    {
        private readonly IWindowFactory m_windowFactory;
        private ICommand m_openNewWindow;

        public ViewManager(IWindowFactory windowFactory)
        {
            m_windowFactory = windowFactory;
            m_openNewWindow = null;
        }

        public void OpenWindow()
        {
            m_windowFactory.CreateNewWindow();
        }

        public ICommand OpenWindowCommand { get { return m_openNewWindow; } }
    }

    public interface IWindowFactory
    {
        void CreateNewWindow();
    }

    public class ProductionWindowFactory : IWindowFactory
    {
        public void CreateNewWindow()
        {
            MainWindow window = new()
            {
                DataContext = new MainWindowViewModel()
            };
            window.Show();
        }
    }
}
