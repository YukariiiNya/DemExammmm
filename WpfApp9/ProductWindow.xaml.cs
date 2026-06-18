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
using WpfApp9.Statics;
using WpfApp9.ViewModels;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private rewEntities _db = new rewEntities();
        private List<ProductViewModel> _productViewModels = new List<ProductViewModel>();
        private string[] _sortingTypes = { "По умолчанию", "По возрастанию", "По убыванию" };
        private List<string> _filteringTypes = new List<string> { "Все скидки", "0-9%", "10-14%", "15%"};

        public ProductWindow()
        {
            InitializeComponent();
            LoadData();
            LoadUi();
            LoadProducts();
        }

        private void LoadUi()
        {
            var user = CurrentSession.currentUser as User;
            if (user != null)
            {
                FullUserName.Text = $"{user.Surname} {user.Name} {user.Patronmic}";
            }
            if (user == null || user.RoleId == 3)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
            }
            else if(user.RoleId == 1)
            {
                CreateButton.Visibility = Visibility.Visible;
            }
        }

        private void LoadProducts()
        {
            var products = _db.Product.ToList();
            _productViewModels = products.Select(p => new ProductViewModel(p)).ToList();
            UpdateProducts();
        }

        private void UpdateProducts()
        {
            var result = _productViewModels.ToList();
            string searchingText = SearchTextBox.Text?.ToLower();
            if (!string.IsNullOrWhiteSpace(searchingText))
            {
                result = result.Where(p => p.Category.Name.ToLower().Contains(searchingText) ||
                p.Name.ToLower().Contains(searchingText) ||
                p.Provider.Name.ToLower().Contains(searchingText) ||
                p.Producer.Name.ToLower().Contains(searchingText) ||
                p.Description.ToLower().Contains(searchingText) ||
                p.Unit.Name.ToLower().Contains(searchingText)).ToList();
            }
            ProductList.ItemsSource = result;
            int sortingType = SortingComboBox.SelectedIndex;
            if (sortingType == 1)
            {
                result = result.OrderByDescending(p => p.AmountInStock).ToList();
            }
            if(sortingType == 2)
            {
                result = result.OrderBy(p => p.AmountInStock).ToList();
            }
            ProductList.ItemsSource = result;
            string filterText = FilterComboBox.SelectedValue?.ToString();
            if(filterText == "0-9%")
            {
                result = result.Where(p => p.Discount >= 0 && p.Discount <= 10).ToList();
            }
            else if (filterText == "10-14%")
            {
                result = result.Where(p => p.Discount >= 10 && p.Discount <= 14).ToList();
            }
            else if (filterText == "15%")
            {
                result = result.Where(p => p.Discount >= 15).ToList();
            }
            ProductList.ItemsSource = result;
        }

        private void LoadData()
        {
            SortingComboBox.ItemsSource = _sortingTypes;
            FilterComboBox.ItemsSource = _filteringTypes;
            SortingComboBox.SelectedIndex = 0;
            FilterComboBox.SelectedIndex = 0;

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateProducts();

        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProducts();
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProducts();

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User user = CurrentSession.currentUser;
            if(user.RoleId == 1)
            {
                int id = (int)((Border)sender).Tag;
                AddEditWindow addEditWindow = new AddEditWindow(id);
                addEditWindow.Show();
                this.Close();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow addEditWindow = new AddEditWindow(null);
            addEditWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            this.Close();
        }
    }
}
