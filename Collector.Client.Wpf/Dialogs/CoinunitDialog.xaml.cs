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
    /// Interaction logic for CoinunitDialog.xaml
    /// </summary>
    public partial class CoinunitDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Coin? _coin;
        public CoinunitDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CoinunitDialog_ContentRendered;
            Closing += CoinunitDialog_Closing;
            KeyDown += CoinunitDialog_KeyDown;
            App.EventHelper.CoinListChanged += EventHelper_CoinListChanged;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillCoinControl();
            Mouse.OverrideCursor = c;
        }
        private void EventHelper_CoinListChanged(object sender, EventArgs e)
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillCoinControl();
            Mouse.OverrideCursor = c;
        }

        private void FillCoinControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            dgCoinunits.BeginInit();
            dgCoinunits.ItemsSource = App.DataShare.CoinList;
            dgCoinunits.EndInit();
            Mouse.OverrideCursor = c;
        }

        private void DoSave(bool CloseAfter)
        {
            _coin = DataContext as Entity.Coin;
            if (_coin != null)
            {
                if (_coin.Id == 0)
                {
                    _coin = App.AppShare.Repositories.CoinRepository.Create(_coin);
                }
                else
                {
                    _coin = App.AppShare.Repositories.CoinRepository.Update(_coin);
                }
                DataContext = _coin;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetFilterview()
        {
            if (App.DataShare.CoinList == null) return;
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            if (txtFilter.Text == "")
            {
                FillCoinControl();
            }
            else
            {
                dgCoinunits.BeginInit();
                dgCoinunits.ItemsSource = App.DataShare.CoinList.Where(x => x.Value.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
                dgCoinunits.EndInit();
            }
            Mouse.OverrideCursor = c;
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_coin != null)
            {
                if (_coin.Id == 0)
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

        private void CoinunitDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
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

        private void CoinunitDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CoinunitDialog_ContentRendered(object? sender, EventArgs e)
        {
            _coin = DataContext as Entity.Coin;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _coin = new Entity.Coin();
            DataContext = _coin;
            SetGui();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Coinunit?", "Delete Coinunit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            _coin = DataContext as Entity.Coin;
            if (_coin != null)
            {
                App.AppShare.Repositories.CoinRepository.Delete(_coin.Id);
                _coin = null;
                DataContext = _coin;
                SetGui();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterview();
        }

        private void dgCoinunits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _coin = dgCoinunits.SelectedItem as Entity.Coin;
            DataContext = _coin;
            SetGui();
        }
    }
}
