using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace Vesna.UsersModel
{
    public class UsersDataService
    {
        #region Public Fields
        /// <summary>
        /// Коллекция пользователей
        /// </summary>
        public static ObservableCollection<User> UsersCollection { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UsersDataService() {}

        /// <summary>
        /// Конструктор типа
        /// </summary>
        static UsersDataService()
        {
            UsersCollection = new ObservableCollection<User>();
            GetUsers();
        }

        #endregion

        /// <summary>
        /// Получение пользователей
        /// </summary>
        public static void GetUsers()
        {
            //Очищаем коллекцию пользователей
            UsersCollection.Clear();

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                //Открываем подключение
                connection.Open();
                using (var reader = new MySqlCommand("SELECT * FROM Users", connection).ExecuteReader())
                {
                    //Пока данные в таблице есть
                    while (reader.Read())
                    {
                        //Добавляем пользователя в UI потоке
                        Application.Current.Dispatcher.Invoke(() => UsersCollection.Add(new User()
                        {
                            ID = reader.GetInt64("ID"),
                            FIO = reader.GetString("FIO"),
                            Email = reader.GetString("Email"),
                            Phone = reader.GetString("Phone"),
                            Login = reader.GetString("Login"),
                            Password = reader.GetString("Password"),
                            Role = (Roles)reader.GetInt64("Role")
                        }));

                    }
                }
            }
        }
        

        public static void AddUser(User newUser)
        {
            if (newUser == null)
                return;

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"INSERT INTO Users(FIO, Email, Phone, Login, Password, Role) VALUES('{newUser.FIO}', '{newUser.Email}', '{newUser.Phone}', '{newUser.Login}', '{newUser.Password}', {(long)newUser.Role})";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                UsersCollection.Clear();

                GetUsers();
            }
        }

        public void ChangeUser(User changedUser)
        {
            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"UPDATE Users SET FIO = '{changedUser.FIO}', Email = '{changedUser.Email}', Phone = '{changedUser.Phone}', Login = '{changedUser.Login}', Password = '{changedUser.Password}' WHERE ID = {changedUser.ID}";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                GetUsers();
            }
        }

        public void DeleteUser(long id)
        {
            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"DELETE FROM Users WHERE ID = {id}";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                GetUsers();
            }
        }
    }
}
