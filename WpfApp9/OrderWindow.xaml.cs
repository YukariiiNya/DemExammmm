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
using WpfApp9.ViewModels;

namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private rewEntities _db = new rewEntities();
        private List<OrderViewModel> _orderViewModels = new List<OrderViewModel>().ToList();
        public OrderWindow()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _db.Order.ToList();
            _orderViewModels = orders.Select(o => new OrderViewModel(o)).ToList();
            UpdateOrders();
        }

        private void UpdateOrders()
        {
            var result = _orderViewModels.ToList();
            OrderList.ItemsSource = result;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
