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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp9.Helpers;
using WpfApp9.Statics;
namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private rewEntities _db = new rewEntities();
        private MessageHelper _mh = new MessageHelper();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginEnter_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginEnter.Text;
            var password = PasswordEnter.Password;
            var user = _db.User.FirstOrDefault(u => u.Login == login && u.Password == password);
            if(user == null)
            {
                _mh.ShowError("Неверный логин или пароль");
                return;
            }
            CurrentSession.currentUser = user;
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();
        }
    }
}
