using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Vesna.Clients
{
    public class ClientsDataService
    {
        #region Public Fields

        /// <summary>
        /// Коллекция клиентов
        /// </summary>
        public static ObservableCollection<Client> Clients { get; set; }

        #endregion

        static ClientsDataService()
        {
            Clients = new ObservableCollection<Client>();
        }

        public ClientsDataService()
        {
            if (Clients.Count > 0)
                return;
            Refresh();
        }

        public void Refresh()
        {

            Clients.Clear();

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                connection.Open();
                using (var reader = new MySqlCommand("SELECT * FROM Clients", connection).ExecuteReader())
                {
                    if (!reader.HasRows)
                        return;

                    while (reader.Read())
                    {
                        Clients.Add(new Client()
                        {
                            ID = reader.GetInt64("ID"),
                            Name = reader.GetString("Name"),
                            Adress = reader.GetString("Adress"),
                            Phone = reader.GetString("Phone")
                        });
                    }
                }
                connection.Close();
            }
        }

        public void AddClient(Client client)
        {
            if (client == null)
                return;

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"INSERT INTO Clients(Name, Adress, Phone) VALUES('{client.Name}', '{client.Adress}', '{client.Phone}')";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                Refresh();
            }
        }

        public void ChangeClient(Client client)
        {
            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                var query = $"UPDATE Clients SET Name = '{client.Name}', Adress = '{client.Adress}', Phone = '{client.Phone}' WHERE ID = {client.ID}";

                connection.Open();

                new MySqlCommand(query, connection).ExecuteNonQuery();

                Refresh();
            }
        }
    }
}
