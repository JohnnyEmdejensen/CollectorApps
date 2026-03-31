using Collector.Client.Wpf.Internals;
using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for LocationDialog.xaml
    /// </summary>
    public partial class LocationDialog : Window
    {
        private Internals.FormSettingsClass _setings;
        private Entity.Location? _location;
        public LocationDialog()
        {
            InitializeComponent();
            _setings = new FormSettingsClass(this);
            ContentRendered += LocationDialog_ContentRendered;
            Closing += LocationDialog_Closing;
            KeyDown += LocationDialog_KeyDown;
            App.EventHelper.LocationListChanged += EventHelper_LocationListChanged;
            FillControls();
            _setings.Load();
        }

        private void DoAdd()
        {
            _location = new Entity.Location();
            DataContext = _location;
            SetGui();
            txtName.Focus();
        }
        private void DoSave(bool CloseAfter)
        {
            gsMain.Focus();
            _location = DataContext as Entity.Location;
            if (_location != null)
            {
                if (_location.Id == 0)
                {
                    _location = App.AppShare.Repositories.LocationRepository.Create(_location);
                }
                else
                {
                    _location = App.AppShare.Repositories.LocationRepository.Update(_location);
                }
                DataContext = _location;
                SetGui();  
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillLocationControl();
            }
        }
        private void FillLocationControl()
        {
            using (new WorkerHandler("FillLocationControl"))
            {
                dgLocations.BeginInit();
                dgLocations.ItemsSource = App.DataShare.LocationList;
                dgLocations.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_location != null)
            {
                if (_location.Id == 0)
                {
                    btnSave.IsEnabled = true;
                    txtName.Focus();
                }
                else
                {
                    btnAdd.IsEnabled = true;
                    btnSave.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                }
            }
            else
            {
                btnAdd.IsEnabled = true;
            }
        }
        private void LocationDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.N)
                {
                    DoAdd();
                }
                if (e.Key == Key.S)
                {
                    DoSave(false);
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    DoSave(true);
                }
            }
        }

        private void LocationDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _setings.Save();
        }

        private void LocationDialog_ContentRendered(object? sender, EventArgs e)
        {
            _location = DataContext as Entity.Location;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnAdd_Click"))
            {
                DoAdd();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnSave_Click"))
            {
                DoSave(false);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _location = dgLocations.SelectedItem as Entity.Location;
            DataContext = _location;
            SetGui();
            txtName.Focus();
        }
        private void EventHelper_LocationListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_LocationListChanged"))
            {
                FillLocationControl();
            }
        }
    }
}
