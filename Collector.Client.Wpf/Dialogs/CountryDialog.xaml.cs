using System;
using System.Collections.Generic;
using System.IO;
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
        private System.Timers.Timer _timer;
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
            _timer = new();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
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
            btnLoadImage.IsEnabled = false;
            btnPasteImage.IsEnabled = false;
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
                    btnLoadImage.IsEnabled = true;
                    if (Clipboard.ContainsImage())
                    {
                        btnPasteImage.IsEnabled = true;
                    }
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
            if (_country != null && _country.FlagImage != null)
            {
                
            }
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
            if (MessageBox.Show("Are you sure to delete the Country?", "Delete Country", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

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
                if (_country != null && _country.FlagImage != null)
                {
                    imgFlag.Source = App.ToolHelper.ConvertByteArrayToBitmapImage(_country.FlagImage);
                }
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

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPasteImage_Click(object sender, RoutedEventArgs e)
        {
            _country = DataContext as Entity.Country;
            if (_country == null) return;

            BitmapSource imgSrc = Clipboard.GetImage();
            imgFlag.Source = imgSrc;

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(imgSrc));
            encoder.Save(memoryStream);
            memoryStream.Position = 0;
            bImg.BeginInit();
            bImg.StreamSource = memoryStream;
            bImg.EndInit();
            var data = App.ToolHelper.ConvertImageToByteArray(bImg);
            _country.FlagImage = data;
            _country =  App.AppShare.Repositories.CountryRepository.Update(_country);
            DataContext = _country;
        }
        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            if (Clipboard.ContainsImage())
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnPasteImage.IsEnabled = true;
                }));
            }
            else
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnPasteImage.IsEnabled = false;
                }));
            }
            _timer.Enabled = true;
        }
    }
}
