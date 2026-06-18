using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order order)
        {
            this.Id = order.Id;
            this.CreationDate = order.CreationDate;
            this.DeliveryDate = order.DeliveryDate;
            this.ReceiptCode = order.ReceiptCode;
            this.StatusText = order.OrderStatus?.Name;
            if(order.PickUpPoint != null)
            {
                this.AddressText = $"{order.PickUpPoint.City} {order.PickUpPoint.Street} {order.PickUpPoint.Building}";
            }
            var firstProductItem = order.ProductInOrder.FirstOrDefault();
            this.ProductArticleText = order.ProductInOrder.FirstOrDefault()?.Product?.Article ?? "Нет товара";
        }
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string ReceiptCode { get; set; }

        // Свойства для вывода текста в XAML
        public string ProductArticleText { get; set; }
        public string StatusText { get; set; }
        public string AddressText { get; set; }
    }
}
