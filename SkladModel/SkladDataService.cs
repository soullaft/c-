using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Vesna.SkladModel
{
    /// <summary>
    /// Представляет логику склада
    /// </summary>
    public class SkladDataService
    {

        #region Public Fields
        /// <summary>
        /// Коллекция товаров
        /// </summary>
        public static ObservableCollection<Item> ItemsCollection { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор типа
        /// </summary>
        static SkladDataService()
        {

            ItemsCollection = new ObservableCollection<Item>();

            GetItems();
        }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public SkladDataService() { }

        #endregion

        #region Work with DataBase

        /// <summary>
        /// Добавляет новый предмет на склад
        /// </summary>
        /// <param name="newItem">новый предмет</param>
        public void AddItem(Item newItem)
        {
            if (newItem == null)
                return;

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"INSERT INTO Sklad(Name, Count, Cost) VALUES('{newItem.Name}', {newItem.Count}, {newItem.Money})";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                GetItems();
            }
        }

        /// <summary>
        /// Изменяет информацию о предмете
        /// </summary>
        /// <param name="changedItem">измененный предмет</param>
        public void ChangeItem(Item changedItem)
        {
            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"UPDATE Sklad SET Name = '{changedItem.Name}', Count = {changedItem.Count}, Cost = {changedItem.Money} WHERE ID = {changedItem.ID}";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                GetItems();
            }
        }


        /// <summary>
        /// Удалить товар со склада
        /// </summary>
        /// <param name="id">идентификатор товара</param>
        public void DeleteItem(long id)
        {
            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"DELETE FROM Sklad WHERE ID = {id}";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                GetItems();
            }
        }

        /// <summary>
        /// Обновить список товаров
        /// </summary>
        public void RefreshItems()
        {
            GetItems();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Получает товары из базы данных
        /// </summary>
        private static void GetItems()
        {

            ItemsCollection.Clear();

            Task.Run(() =>
            {
                using (var connection = new MySqlConnection(MyDB.ConnectionString))
                {
                    connection.Open();
                    using (var reader = new MySqlCommand("SELECT * FROM Sklad", connection).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Application.Current.Dispatcher.Invoke(() => ItemsCollection.Add(new Item()
                            {
                                ID = reader.GetInt64("ID"),
                                Name = reader.GetString("Name"),
                                Count = reader.GetInt64("Count"),
                                Money = reader.GetDouble("Cost"),
                            }));
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Возвращает предмет со склада
        /// </summary>
        /// <param name="id">Идентификатор предмета</param>
        /// <returns></returns>
        private Item GetItem(long id) => ItemsCollection.Where(item => item.ID == id).First();

        #endregion
    }
}
