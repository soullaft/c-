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

namespace Vesna
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();

            server.Text = Properties.Settings.Default.server;
            database.Text = Properties.Settings.Default.database;
            user.Text = Properties.Settings.Default.user;
            password.Password = Properties.Settings.Default.password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMessageAsync("Уведомление", "Данные были успешно сохранены", MessageDialogStyle.Affirmative);

            Properties.Settings.Default.server = server.Text;
            Properties.Settings.Default.database = database.Text;
            Properties.Settings.Default.user = user.Text;
            Properties.Settings.Default.password = password.Password;
            Properties.Settings.Default.Save();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
