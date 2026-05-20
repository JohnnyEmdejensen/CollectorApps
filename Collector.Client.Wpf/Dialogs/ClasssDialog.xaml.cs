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
    /// Interaction logic for ClasssDialog.xaml
    /// </summary>
    public partial class ClasssDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Classs? _classs;

        public ClasssDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += ClasssDialog_ContentRendered;
            Closing += ClasssDialog_Closing;
            KeyDown += ClasssDialog_KeyDown;
            App.EventHelper.ClasssListChanged += EventHelper_ClasssListChanged;
            FillControls();
            _settings.Load();
        }


        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillClasses();
            }
        }
        private void FillClasses()
        {
            using (new WorkerHandler("FillClasses"))
            {
                dgClasses.BeginInit();
                dgClasses.ItemsSource = App.DataShare.ClasssList;
                dgClasses.EndInit();
            }
        }
        private void DoAdd()
        {
            _classs = new Classs();
            DataContext = _classs;
            SetGui();
            txtName.Focus();
        }
        private void DoSave(bool CloseAfter)
        {
            if (_classs == null) return;
            if (_classs.Id == 0)
            {
                _classs = App.AppShare.Repositories.ClasssRepository.Create(_classs);
            }
            else
            {
                _classs = App.AppShare.Repositories.ClasssRepository.Update(_classs);
            }
            DataContext = _classs;
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
            if (_classs != null)
            {
                if (_classs.Id == 0)
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
        private void ClasssDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void ClasssDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void ClasssDialog_ContentRendered(object? sender, EventArgs e)
        {
            _classs = DataContext as Entity.Classs;
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

        private void dgClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _classs = dgClasses.SelectedItem as Entity.Classs;
            DataContext = _classs;
            SetGui();
            txtName.Focus();
            txtName.SelectAll();
        }
        private void EventHelper_ClasssListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_ClasssListChanged"))
            {
                FillClasses();
            }
        }
    }
}
