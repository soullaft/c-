using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using Vesna.Clients;

namespace Vesna.Pages
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : UserControl
    {

        private ObservableCollection<Client> result = new ObservableCollection<Client>();

        public Clients()
        {
            InitializeComponent();

            DataContext = new ClientsDataService();
        }

        /// <summary>
        /// Показать добавление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect()
            {
                Radius = 5,
                RenderingBias = RenderingBias.Quality
            };

            ClientsPanel.Effect = effect;
            ClientsPanel.IsEnabled = false;

            grid.Visibility = Visibility.Visible;

            addItem.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowAdd", delegate { });
        }

        /// <summary>
        /// Отменить добавление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanselAdd_Click(object sender, RoutedEventArgs e)
        {
            AnimationHelper.StartAnimation(this, "HideAdd", delegate
            {
                clientName.Text = string.Empty;
                clientAdress.Text = string.Empty;
                clientPhone.Text = string.Empty;
                ClientsPanel.Effect = null;
                ClientsPanel.IsEnabled = true;
                grid.Visibility = Visibility.Collapsed;
                ClientsPanel.IsEnabled = true;

                addItem.IsEnabled = true;
            });
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(clientName.Text) && string.IsNullOrEmpty(clientAdress.Text) && string.IsNullOrEmpty(clientPhone.Text))
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Поля должны быть заполнены", MessageDialogStyle.Affirmative);
                return;
            }
            new ClientsDataService().AddClient(new Client()
            {
                Name = clientName.Text,
                Adress = clientAdress.Text,
                Phone = clientPhone.Text
            });

            CanselAdd_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchClient_Changed(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text;

            if (string.IsNullOrEmpty(searchText))
            {
                ClientsPanel.ItemsSource = null;
                ClientsPanel.ItemsSource = ClientsDataService.Clients;

                return;
            }

            result.Clear();

            foreach (var item in ClientsDataService.Clients.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper())))
                result.Add(item);

            ClientsPanel.ItemsSource = null;
            ClientsPanel.ItemsSource = result;
        }

        /// <summary>
        /// Обновить клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ClientsDataService().Refresh();
        }

        /// <summary>
        /// Проверка на числы в добавлении клиента(Телефон)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientPhone_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
