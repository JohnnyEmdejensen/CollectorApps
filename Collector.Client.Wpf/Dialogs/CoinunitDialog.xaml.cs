using Collector.Entity;
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
            _settings.Load();
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

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgCoinunits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
