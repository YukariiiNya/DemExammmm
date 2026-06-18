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
using WpfApp9.Helpers;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private rewEntities _db = new rewEntities(); //
        private bool _isEditing; // Флаг редактирования
        private Product _product; // Товар из базы данных
        private MessageHelper _mh = new MessageHelper();

        public AddEditWindow(int? id) //
        {
            InitializeComponent();
            if(id == null)
            {
                _isEditing = false;
            }
            else
            {
                _isEditing = true;
                _product = _db.Product.FirstOrDefault(p => p.Id == id);
            }
            LoadData();
            if (_isEditing)
            {
                FillData();
            }

        }

        private void LoadData()
        {
            ProductUnit.ItemsSource = _db.Unit.ToList();
            ProductProvider.ItemsSource = _db.Provider.ToList();
            ProductProducer.ItemsSource = _db.Producer.ToList();
            ProductCategory.ItemsSource = _db.Category.ToList();
            ProductUnit.SelectedIndex = 0;
            ProductProvider.SelectedIndex = 0;
            ProductProducer.SelectedIndex = 0;
            ProductCategory.SelectedIndex = 0;
        }

        private void FillData()
        {
            ProductArticle.Text = _product.Article;
            ProductName.Text = _product.Name;
            ProductPrice.Text = _product.Price.ToString();
            ProductDiscount.Text = _product.Discount.ToString();
            ProductInStock.Text = _product.AmountInStock.ToString();
            ProductDescription.Text = _product.Description;
            ProductPhoto.Text = _product.Photo;
            ProductUnit.SelectedValue = _product.UnitId;
            ProductProvider.SelectedValue = _product.ProviderId;
            ProductProducer.SelectedValue = _product.ProducerId;
            ProductCategory.SelectedValue = _product.CategoryId;
        }

        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isEditing)
                {
                    UpdateProduct();
                    _mh.ShowInfo("Продукт успешно изменен");
                }
                else
                {
                    CreateProduct();
                    _mh.ShowInfo("Продукт успешно добавлен");
                }
                CancelButton_Click(null, null);
            }
            catch(Exception ex)
            {
                _mh.ShowError(ex.Message);
            }
        }

        private void UpdateProduct()
        {
            _product.Article = ProductArticle.Text;
            _product.Name = ProductName.Text;
            _product.Price = Convert.ToDecimal(ProductPrice.Text);
            _product.Discount = Convert.ToDecimal(ProductDiscount.Text);
            _product.AmountInStock = Convert.ToDecimal(ProductInStock.Text);
            _product.Description = ProductDescription.Text;
            _product.UnitId = (int)ProductUnit.SelectedValue;
            _product.ProviderId = (int)ProductProvider.SelectedValue;
            _product.ProducerId = (int)ProductProducer.SelectedValue;
            _product.CategoryId = (int)ProductCategory.SelectedValue;
            _db.SaveChanges();
        }
        private void CreateProduct()
        {
            Product product = new Product
            {
                Article = ProductArticle.Text,
                Name = ProductName.Text,
                Price = Convert.ToDecimal(ProductPrice.Text),
                Discount = Convert.ToDecimal(ProductDiscount.Text),
                AmountInStock = Convert.ToDecimal(ProductInStock.Text),
                UnitId = (int)ProductUnit.SelectedValue,
                ProviderId = (int)ProductProvider.SelectedValue,
                ProducerId = (int)ProductProducer.SelectedValue,
                CategoryId = (int)ProductCategory.SelectedValue
                
            };
            _db.Product.Add(product);
            _db.SaveChanges();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(); //
            productWindow.Show();
            this.Close(); //
        }
    }
}
    
