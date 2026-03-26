using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Category? _category;
        public CategoryDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CategoryDialog_ContentRendered;
            Closing += CategoryDialog_Closing;
            KeyDown += CategoryDialog_KeyDown;
            dgCategories.SizeChanged += DgCategories_SizeChanged;
            App.EventHelper.CategoryListChanged += EventHelper_CategoryListChanged;
            FillControls();
            _settings.Load();
        }


        private void DoSave(bool CloseAfter)
        {
            _category = DataContext as Entity.Category;
            if (_category != null)
            {
                if (_category.Id == 0)
                {
                    _category = App.AppShare.Repositories.CategoryRepository.Create(_category);
                }
                else
                {
                    _category = App.AppShare.Repositories.CategoryRepository.Update(_category);
                }
                DataContext = _category;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }

        private void FillControls()
        {
            dgCategories.BeginInit();
            dgCategories.ItemsSource = App.DataShare.CategoryList;
            dgCategories.EndInit();
        }
        private void SetFilterView()
        {
            if (App.DataShare.CategoryList == null) return;
            if (txtFilter.Text == "")
            {
                FillControls();
            }
            else
            {
                dgCategories.BeginInit();
                dgCategories.ItemsSource = App.DataShare.CategoryList.Where(x => x.Value.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
                dgCategories.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_category != null)
            {
                if (_category.Id == 0)
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
        private void CategoryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(false);
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(true);
                }
            }
        }

        private void CategoryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CategoryDialog_ContentRendered(object? sender, EventArgs e)
        {
            _category = DataContext as Entity.Category;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
            _category = new Entity.Category();
            DataContext = _category;
            SetGui();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Category?", "Delete Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            _category = DataContext as Entity.Category;
            if (_category != null)
            {
                App.AppShare.Repositories.CategoryRepository.Delete(_category.Id);
                _category = null;
                DataContext = _category;
                SetGui();
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterView();
        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _category = dgCategories.SelectedItem as Entity.Category;
            DataContext = _category;
            SetGui();
        }
        private void EventHelper_CategoryListChanged(object sender, EventArgs e)
        {
            FillControls();
        }
        private void DgCategories_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgCategories.ActualWidth - 8 - 24;
            dgCategories.Columns[1].Width = width / 2;
            dgCategories.Columns[2].Width = width / 2;
            dgCategories.Columns[3].Width = 24;
        }
    }
}
