using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using Vesna.UsersModel;

namespace Vesna.Pages
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {

        #region Private Fields

        private User selectedUser;

        private ObservableCollection<User> result = new ObservableCollection<User>();

        #endregion

        public Users()
        {
            InitializeComponent();

            DataContext = new UsersDataService();
        }

        private void RefreshUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersDataService.GetUsers();
        }

        /// <summary>
        /// Показать форму добавления пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUserShow_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect()
            {
                Radius = 5,
                RenderingBias = RenderingBias.Quality
            };

            dataGrid.Effect = effect;
            dataGrid.IsEnabled = false;

            grid.Visibility = Visibility.Visible;

            addItem.IsEnabled = false;
            editItem.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowAdd", delegate { });
        }

        /// <summary>
        /// Отменить добавление пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideAddUserGrid_Click(object sender, RoutedEventArgs e)
        {
            AddCanselGrid.IsEnabled = false;
            AnimationHelper.StartAnimation(this, "HideAdd", delegate
            {
                userFIO.Text = string.Empty;
                userEMAIL.Text = string.Empty;
                userPHONE.Text = string.Empty;
                userLOGIN.Text = string.Empty;
                userPASSWORD.Password = string.Empty;
                dataGrid.Effect = null;
                dataGrid.IsEnabled = true;
                grid.Visibility = Visibility.Collapsed;
                AddCanselGrid.IsEnabled = true;

                addItem.IsEnabled = true;
                editItem.IsEnabled = true;
            });
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text;

            if (string.IsNullOrEmpty(searchText))
            {
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = UsersDataService.UsersCollection;

                return;
            }

            result.Clear();

            foreach (var item in UsersDataService.UsersCollection.Where(x => x.FIO.ToUpper().Contains(searchText.ToUpper())))
                result.Add(item);

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = result;
        }

        private void ChangeUserShow_Click(object sender, RoutedEventArgs e)
        {
            selectedUser = dataGrid.SelectedItem as User;

            if (selectedUser == null)
                return;

            editItemGrid.Visibility = Visibility.Visible;

            BlurEffect effect = new BlurEffect()
            {
                Radius = 5,
                RenderingBias = RenderingBias.Quality
            };

            addItem.IsEnabled = false;
            editItem.IsEnabled = false;

            dataGrid.Effect = effect;
            dataGrid.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowEditItem", delegate { });

            edituserFIO.Text = selectedUser.FIO;
            edituserEMAIL.Text = selectedUser.Email;
            edituserPHONE.Text = selectedUser.Phone;
            edituserLOGIN.Text = selectedUser.Login;
            edituserPASSWORD.Password = selectedUser.Password;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userFIO.Text) && string.IsNullOrEmpty(userEMAIL.Text) && string.IsNullOrEmpty(userPHONE.Text) && string.IsNullOrEmpty(userLOGIN.Text) && string.IsNullOrEmpty(userPASSWORD.Password))
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Поля должны быть заполнены", MessageDialogStyle.Affirmative);
                return;
            }

            int role;
            if (userRoleAdmin.IsChecked == true)
                role = 1;
            else
                role = 2;
            UsersDataService.AddUser(new User()
            {
                FIO = userFIO.Text.Trim(),
                Email = userEMAIL.Text.Trim(),
                Phone = userPHONE.Text.Trim(),
                Login = userLOGIN.Text.Trim(),
                Password = userPASSWORD.Password.Trim(),
                Role = (Roles)role,
            });

            HideAddUserGrid_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Отменить редактирование пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanselEditUser_Click(object sender, RoutedEventArgs e)
        {
            AnimationHelper.StartAnimation(this, "HideEditItem", delegate
            {
                dataGrid.Effect = null;
                dataGrid.IsEnabled = true;
                editItemGrid.Visibility = Visibility.Collapsed;

                addItem.IsEnabled = true;
                editItem.IsEnabled = true;
            });
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(edituserFIO.Text) && string.IsNullOrEmpty(edituserEMAIL.Text) && string.IsNullOrEmpty(edituserPHONE.Text) && string.IsNullOrEmpty(edituserLOGIN.Text) && string.IsNullOrEmpty(edituserPASSWORD.Password))
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Поля должны быть заполнены", MessageDialogStyle.Affirmative);
                return;
            }

            selectedUser.FIO = edituserFIO.Text;
            selectedUser.Email = edituserEMAIL.Text;
            selectedUser.Phone= edituserPHONE.Text;
            selectedUser.Login = edituserLOGIN.Text;
            selectedUser.Password = edituserPASSWORD.Password;

            new UsersDataService().ChangeUser(selectedUser);

            CanselEditUser_Click(this, new RoutedEventArgs());
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            new UsersDataService().DeleteUser((long)selectedUser.ID);

            CanselEditUser_Click(this, new RoutedEventArgs());
        }
    }
}
