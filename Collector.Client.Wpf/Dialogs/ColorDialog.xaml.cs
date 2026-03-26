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
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Color? _color;
        public ColorDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += ColorDialog_ContentRendered;
            Closing += ColorDialog_Closing;
            KeyDown += ColorDialog_KeyDown;
            dgColors.SizeChanged += DgColors_SizeChanged;
            App.EventHelper.ColorListChanged += EventHelper_ColorListChanged;
            _settings.Load();
            FillColorControl();
        }

        private void FillColorControl()
        {
            dgColors.BeginInit();
            dgColors.ItemsSource = App.DataShare.ColorList;
            dgColors.EndInit();
        }

        private void DoAdd()
        {
            _color = new Entity.Color();
            DataContext = _color;
            SetGui();
        }
        private void DoSave(bool CloseAfter)
        {
            _color = DataContext as Entity.Color;
            if (_color != null)
            {
                if (_color.Id == 0)
                {
                    _color = App.AppShare.Repositories.ColorRepository.Create(_color);
                }
                else
                {
                    _color = App.AppShare.Repositories.ColorRepository.Update(_color);
                }
                DataContext = _color;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_color != null)
            {
                if (_color.Id == 0)
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
        private void ColorDialog_KeyDown(object sender, KeyEventArgs e)
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
                if (e.Key == Key.N)
                {
                    DoAdd();
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

        private void ColorDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void ColorDialog_ContentRendered(object? sender, EventArgs e)
        {
            _color = DataContext as Entity.Color;
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

        }

        private void dgColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _color = dgColors.SelectedItem as Entity.Color;
            DataContext = _color;
            SetGui();
            txtName.Focus();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void EventHelper_ColorListChanged(object sender, EventArgs e)
        {
            FillColorControl();
        }
        private void DgColors_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgColors.ActualWidth - 2;
            dgColors.Columns[1].Width = width;
        }
    }
}
