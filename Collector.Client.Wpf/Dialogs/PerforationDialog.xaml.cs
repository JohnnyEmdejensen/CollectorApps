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
    /// Interaction logic for PerforationDialog.xaml
    /// </summary>
    public partial class PerforationDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Perforation? _perforation;
        public PerforationDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PerforationDialog_ContentRendered;
            Closing += PerforationDialog_Closing;
            KeyDown += PerforationDialog_KeyDown;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            dgPerforations.BeginInit();
            dgPerforations.ItemsSource = App.DataShare.PerforationList;
            dgPerforations.EndInit();
        }
        private void DoAdd()
        {
            _perforation = new Entity.Perforation();
            DataContext = _perforation;
            SetGui();
            txtName.Focus();
        }

        private void DoSave(bool CloseAfter)
        {
            if (_perforation == null) return;
            if (_perforation.Id == 0)
            {
                _perforation = App.AppShare.Repositories.PerforationRepository.Create(_perforation);
            }
            else
            {
                _perforation = App.AppShare.Repositories.PerforationRepository.Update(_perforation);
            }
            DataContext = _perforation;
            SetGui();
            if (CloseAfter)
            {
                Close();
            }
        }

        private void DoDelete()
        {

        }

        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_perforation != null)
            {
                if (_perforation.Id == 0)
                {
                    btnSave.IsEnabled = true;
                    txtName.Focus();
                }
                else
                {
                    btnAdd.IsEnabled = true;
                    btnSave.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    txtFilter.Focus();
                }
            }
            else
            {
                btnAdd.IsEnabled = true;
                txtFilter.Focus();
            }
        }
        private void PerforationDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.S)
            {
                e.Handled = true;
                DoSave(false);
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.N)
            {
                e.Handled = true;
                DoAdd();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) && e.Key == Key.S)
            {
                e.Handled = true;
                DoSave(true);
            }
        }

        private void PerforationDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PerforationDialog_ContentRendered(object? sender, EventArgs e)
        {
            _perforation = DataContext as Entity.Perforation;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DoDelete();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _perforation = dgPerforations.SelectedItem as Entity.Perforation;
            DataContext = _perforation;
            SetGui();
            txtName.Focus();
            txtName.SelectAll();
        }
    }
}
