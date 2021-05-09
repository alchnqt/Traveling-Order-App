using FakeAtlas.FakeAtlasUIComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FakeAtlas.ViewModels.Management
{
    #region preventing mvvm violation custom messagebox service
    interface IMessageBoxService
    {
        bool ShowMessage(string text);
        bool Show();
    }

    class FakeAtlasMessageBoxService : IMessageBoxService
    {
        public bool Show()
        {
            FakeAtlasMessageBox box = new();
            if (box.Show() == MessageBoxResult.OK)
                return true;
            else
                return false;
        }

        public bool ShowMessage(string text)
        {
            FakeAtlasMessageBox box = new();
            if (box.Show(text) == MessageBoxResult.OK)
                return true;
            else
                return false;
        }

        public bool ShowMessage(FakeAtlasMessageBox.MessageType type, ViewModelBase.Localization localization)
        {
            FakeAtlasMessageBox box = new();
            if (box.Show(type, localization) == MessageBoxResult.OK)
                return true;
            else
                return false;
        }
    }
    #endregion
}
