using System;
using System.Collections.Generic;
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
    /// Interaction logic for CountryDialog.xaml
    /// </summary>
    public partial class CountryDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Country? _country;
        private List<ViewModel.CountryModel>? _list;
        public CountryDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CountryDialog_ContentRendered;
            Closing += CountryDialog_Closing;
            KeyDown += CountryDialog_KeyDown;
            App.EventHelper.CountryListChanged += EventHelper_CountryListChanged;
            App.EventHelper.ContinentListChanged += EventHelper_ContinentListChanged;
            dgCountries.SizeChanged += DgCountries_SizeChanged;
            FillContinentControl();
            FillCountryControl();
            _settings.Load();
        }


        private void FillContinentControl()
        {
            Cursor c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboContinent.BeginInit();
            cboContinent.ItemsSource = App.DataShare.ContinentList;
            cboContinent.DisplayMemberPath = "Value";
            cboContinent.SelectedValuePath = "Id";
            cboContinent.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillCountryControl()
        {
            Cursor cc = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            _list = new List<ViewModel.CountryModel>();
            if (App.DataShare.CountryList != null)
            {
                foreach (var item in App.DataShare.CountryList)
                {
                    ViewModel.CountryModel c = new ViewModel.CountryModel();
                    c.Name = item.Name; 
                    c.Id = item.Id;
                    c.Iso = item.Iso;
                    c.Number = item.Number;
                    c.Continent = App.ToolHelper.GetContinentName(item.ContinentId);
                    _list.Add(c);
                }
            }
            dgCountries.BeginInit();
            dgCountries.ItemsSource = _list;
            dgCountries.EndInit();
            Mouse.OverrideCursor = cc;
        }
        private void DoSave()
        {
            _country = DataContext as Entity.Country;
            if (_country != null)
            {
                if (_country.Id == 0)
                {
                    _country = App.AppShare.Repositories.CountryRepository.Create(_country);
                }
                else
                {
                    _country = App.AppShare.Repositories.CountryRepository.Update(_country);
                }
                DataContext = _country;
                SetGui();
            }
        }

        private void SetFilterView()
        {
            if (_list == null) return;

            if (txtFilter.Text == "")
            {
                dgCountries.BeginInit();
                dgCountries.ItemsSource = _list;
                dgCountries.EndInit();
            }
            else
            {
                var filtered = _list.Where(x => x.Name.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
                dgCountries.BeginInit();
                dgCountries.ItemsSource = filtered;
                dgCountries.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_country != null)
            {
                if (_country.Id == 0)
                {
                    btnSave.IsEnabled = true;
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
        private void CountryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CountryDialog_ContentRendered(object? sender, EventArgs e)
        {
            _country = DataContext as Entity.Country;
            SetGui();
        }

        private void EventHelper_ContinentListChanged(object sender, EventArgs e)
        {
            FillContinentControl();
        }

        private void EventHelper_CountryListChanged(object sender, EventArgs e)
        {
            FillCountryControl();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
            _country = new Entity.Country();
            DataContext = _country;
            SetGui();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _country = DataContext as Entity.Country;
            if (_country != null)
            {
                App.AppShare.Repositories.CountryRepository.Delete(_country.Id);
                _country = null;
                DataContext = _country;
                SetGui();
            }
        }

        private void dgCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgCountries.SelectedItem as ViewModel.CountryModel;
            if (item != null && App.DataShare.CountryList != null)
            {
                _country = App.DataShare.CountryList.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _country;
                SetGui();
            }
        }
        private void CountryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (Keyboard.Modifiers.HasFlag( ModifierKeys.Control))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave();
                }
            }
        }
        private void DgCountries_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgCountries.Columns[1].Width = dgCountries.ActualWidth - 156 - 4;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterView();
        }
    }
}
