using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp9.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product)
        {
            this.Id = product.Id;
            this.Article = product.Article;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Discount = product.Discount;
            this.AmountInStock = product.AmountInStock;
            this.Description = product.Description;
            this.Photo = product.Photo;
            this.Category = product.Category;
            this.Provider = product.Provider;
            this.Producer = product.Producer;
            this.Unit = product.Unit;
            GetPhoto();
            GetBackground();
            GetPrice();
        }

        private void GetPrice()
        {
            if(Discount > 0)
            {
                OldPrice = Price;
                Price = OldPrice * (1 - (Discount / 100));
            }
        }

        private void GetBackground()
        {
            if(Discount >= 15)
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#2E8B57");
            }
            else
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#7FFF00");
            }
        }

        private void GetPhoto()
        {
            if (!string.IsNullOrWhiteSpace(Photo))
                return;
            Photo = "/Res/picture.png";
        }

        public int Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal AmountInStock { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public Category Category { get; set; }
        public Producer Producer { get; set; }
        public Provider Provider { get; set; }
        public Unit Unit { get; set; }
        public Brush Background { get; set; }
        public decimal OldPrice { get; set; }
    }
}
