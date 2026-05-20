using Collector.Client.Wpf.Internals;
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
    /// Interaction logic for TypeeDialog.xaml
    /// </summary>
    public partial class TypeeDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Typee? _type;
        public TypeeDialog()
        {            
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += TypeeDialog_ContentRendered;
            Closing += TypeeDialog_Closing;
            KeyDown += TypeeDialog_KeyDown;
            App.EventHelper.TypeeListChanged += EventHelper_TypeeListChanged;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillTypees();
            }
        }
        private void FillTypees()
        {
            using (new WorkerHandler("FillTypees"))
            {
                dgTypees.BeginInit();
                dgTypees.ItemsSource = App.DataShare.TypeeList;
                dgTypees.EndInit();
            }
        }
        private void DoAdd()
        {
            _type = new Typee();
            DataContext = _type;
            SetGui();
            txtName.Focus();
        }

        private void DoSave(bool closeAfter)
        {
            if (_type == null) return;
            if (_type.Id == 0)
            {
                _type = App.AppShare.Repositories.TypeeRepository.Create(_type);
            }
            else
            {
                _type = App.AppShare.Repositories.TypeeRepository.Update(_type);
            }
            DataContext = _type;
            DataContext = _type;
            SetGui();
            if (closeAfter)
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
            if (_type != null)
            {
                if (_type.Id == 0)
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
        private void TypeeDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void TypeeDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void TypeeDialog_ContentRendered(object? sender, EventArgs e)
        {
            _type = DataContext as Entity.Typee;
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

        private void dgTypees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _type = dgTypees.SelectedItem as Entity.Typee;
            DataContext = _type;
            SetGui();
            txtName.Focus();
            txtName.SelectAll();
        }
        private void EventHelper_TypeeListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_TypeeListChanged"))
            {
                FillTypees();
            }
        }
    }
}
