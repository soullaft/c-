using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using Vesna.UsersModel;

namespace Vesna
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrEmpty(login.Text) && string.IsNullOrEmpty(passord.Password))
            {
                this.ShowMessageAsync("Ошибка", "Поля должны быть заполнены", MessageDialogStyle.Affirmative);
                return;
            }

            try
            {
                UsersDataService.GetUsers();

                var getUser = UsersDataService.UsersCollection.Where(user => user.Login == login.Text.Trim() && user.Password == passord.Password.Trim()).FirstOrDefault();

                if (getUser != null)
                {
                    CurrentUser.ID = getUser.ID;

                    MainWindow window = new MainWindow();

                    if (getUser.Role == Roles.Manager)
                        (window.menu.Items[1] as MetroTabItem).Visibility = Visibility.Collapsed;

                    this.Hide();

                    Application.Current.MainWindow = window;

                    window.Show();
                }
                else
                {
                    this.ShowMessageAsync("Ошибка", "Неправильный логин или пароль", MessageDialogStyle.Affirmative);
                    return;
                }
            }
            catch(Exception ex)
            {
                this.ShowMessageAsync("Ошибка", $"{ex.Message}", MessageDialogStyle.Affirmative);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Settings().ShowDialog();
        }

        /// <summary>
        /// По нажатии на клавишу enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passord_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Button_Click(this, new RoutedEventArgs());
        }
    }
}
