using MahApps.Metro.Controls.Dialogs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Vesna.Clients;
using Vesna.SkladModel;
using Vesna.UsersModel;

namespace Vesna.Pages
{
    /// <summary>
    /// Interaction logic for Sklad.xaml
    /// </summary>
    public partial class Sklad : UserControl
    {
        #region Private Fields

        private Item selectedItem;

        private ObservableCollection<Item> result = new ObservableCollection<Item>();

        #endregion

        #region Constuctor
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public Sklad()
        {
            InitializeComponent();

            this.DataContext = new SkladDataService();
        }

        #endregion

        #region Actions
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox).Text;

            if (string.IsNullOrEmpty(searchText))
            {
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = SkladDataService.ItemsCollection;

                return;
            }

            result.Clear();

            foreach (var item in SkladDataService.ItemsCollection.Where(x => x.Name.ToUpper().Contains(searchText.ToUpper())))
                result.Add(item);

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = result;
        }

        /// <summary>
        /// Проверка поля только на числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Открыть форму добавления товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddForm_Click(object sender, RoutedEventArgs e)
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
            sellItem.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowAdd", delegate { });


        }

        /// <summary>
        /// Отменить добавление товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanselAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCanselGrid.IsEnabled = false;
            AnimationHelper.StartAnimation(this, "HideAdd", delegate 
            {
                tovarName.Text = string.Empty;
                tovarCount.Text = string.Empty;
                tovarPrice.Text = string.Empty;
                dataGrid.Effect = null;
                dataGrid.IsEnabled = true;
                grid.Visibility = Visibility.Collapsed;
                AddCanselGrid.IsEnabled = true;

                addItem.IsEnabled = true;
                editItem.IsEnabled = true;
                sellItem.IsEnabled = true;
            });
        }

        /// <summary>
        /// Добавить товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tovarName.Text) && string.IsNullOrEmpty(tovarCount.Text) && string.IsNullOrEmpty(tovarPrice.Text))
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Поля должны быть заполнены", MessageDialogStyle.Affirmative);
                return;
            }
            if (double.TryParse(tovarPrice.Text, out double price))
            {
                new SkladDataService().AddItem(new Item()
                {
                    Name = tovarName.Text,
                    Count = Convert.ToInt64(tovarCount.Text),
                    Money = Convert.ToDouble(tovarPrice.Text)
                });

                tovarName.Text = string.Empty;
                tovarCount.Text = string.Empty;
                tovarPrice.Text = string.Empty;

                CanselAdd_Click(this, new RoutedEventArgs());
            }
            else
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Цена товара должна быть числом!", MessageDialogStyle.Affirmative);
                return;
            }

        }

        /// <summary>
        /// Изменить товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            selectedItem = dataGrid.SelectedItem as Item;

            if (selectedItem == null)
                return;

            editItemGrid.Visibility = Visibility.Visible;

            BlurEffect effect = new BlurEffect()
            {
                Radius = 5,
                RenderingBias = RenderingBias.Quality
            };

            addItem.IsEnabled = false;
            editItem.IsEnabled = false;
            sellItem.IsEnabled = false;

            dataGrid.Effect = effect;
            dataGrid.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowEditItem", delegate { });

            editTovarCount.Text = selectedItem.Count.ToString();
            editTovarName.Text = selectedItem.Name;
            editTovarPrice.Text = selectedItem.Money.ToString();
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            selectedItem.Count = Convert.ToInt64(editTovarCount.Text);
            selectedItem.Name = editTovarName.Text;
            selectedItem.Money = Convert.ToDouble(editTovarPrice.Text);

            new SkladDataService().ChangeItem(selectedItem);

            CanselChanges_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Отменить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanselChanges_Click(object sender, RoutedEventArgs e)
        {
            AnimationHelper.StartAnimation(this, "HideEditItem", delegate
            {
                dataGrid.Effect = null;
                dataGrid.IsEnabled = true;
                editItemGrid.Visibility = Visibility.Collapsed;

                addItem.IsEnabled = true;
                editItem.IsEnabled = true;
                sellItem.IsEnabled = true;
            });
        }

        /// <summary>
        /// Удаляет предмет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            new SkladDataService().DeleteItem((long)selectedItem.ID);

            CanselChanges_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Обновить товары
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            new SkladDataService().RefreshItems();
        }

        #endregion


        /// <summary>
        /// При клике на "Продать товар"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sellItem_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect effect = new BlurEffect()
            {
                Radius = 5,
                RenderingBias = RenderingBias.Quality
            };

            dataGrid.Effect = effect;
            dataGrid.IsEnabled = false;

            sellItemGrid.Visibility = Visibility.Visible;

            addItem.IsEnabled = false;
            editItem.IsEnabled = false;
            sellItem.IsEnabled = false;

            AnimationHelper.StartAnimation(this, "ShowSellItem", delegate { });
        }


        /// <summary>
        /// При изменении кол-ва товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }

        /// <summary>
        /// При отмене продажи товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanseSellItem_Click(object sender, RoutedEventArgs e)
        {
            sellItemCount.Text = string.Empty;

            AnimationHelper.StartAnimation(this, "HideSellItem", delegate
            {
                dataGrid.Effect = null;
                dataGrid.IsEnabled = true;
                sellItemGrid.Visibility = Visibility.Collapsed;
                AddCanselGrid.IsEnabled = true;

                addItem.IsEnabled = true;
                editItem.IsEnabled = true;
                sellItem.IsEnabled = true;
            });
        }

        private void itemSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculate();

            if ((itemSell.SelectedItem as Item).Count <= 0)
                sellItemButton.IsEnabled = false;
        }

        private void Calculate()
        {

            if (!(itemSell.SelectedItem is Item result))
                return;


            if (string.IsNullOrEmpty(sellItemCount.Text))
            {
                costBox.Text = "";
                return;
            }

            costBox.Text = (result.Money * Convert.ToDouble(sellItemCount.Text)).ToString() + "руб.";
        }

        /// <summary>
        /// Продать предмет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellItem_Click(object sender, RoutedEventArgs e)
        {
            var sellItem = itemSell.SelectedItem as Item;

            var client = clientList.SelectedItem as Client;

            if (string.IsNullOrEmpty(sellItemCount.Text))
            {
                costBox.Text = "";
                return;
            }

            var count = Convert.ToInt64(sellItemCount.Text.Trim());


            if (client == null && sellItem == null)
            {
                return;
            }


            if (count > sellItem.Count)
            {
                (Application.Current.MainWindow as MainWindow).ShowMessageAsync("Ошибка", "Некорректные данные", MessageDialogStyle.Affirmative);
                return;
            }

            var newCount = sellItem.Count - count;

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                connection.Open();

                new MySqlCommand($"INSERT INTO History(IDUser, IDClient, Text) VALUES ({CurrentUser.ID}, {client.ID}, 'Менеджер {UsersDataService.UsersCollection.Where(x => x.ID == CurrentUser.ID).First().FIO} " +
                    $"продал {sellItem.Name} ({count} шт.) {client.Name} на сумму {costBox.Text} \n {DateTime.Now}')", connection).ExecuteNonQuery();

                new MySqlCommand($"UPDATE Sklad SET Count = {newCount} WHERE ID = {sellItem.ID}", connection).ExecuteNonQuery();

                connection.Close();

                HistoryService.Refresh();
            }

            CanseSellItem_Click(this, new RoutedEventArgs());

            itemSell.SelectionChanged -= itemSell_SelectionChanged;
            Refresh_Click(this, new RoutedEventArgs());
            itemSell.SelectionChanged += itemSell_SelectionChanged;

        }
    }
}
