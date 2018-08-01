using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Vesna
{
    public class HistoryService
    {
        public static ObservableCollection<Hist> HistoryCollection { get; set; }

        static HistoryService()
        {
            HistoryCollection = new ObservableCollection<Hist>();

            Refresh();
        }

        public static void Refresh()
        {
            HistoryCollection.Clear();

            using (var connection = new MySqlConnection(MyDB.ConnectionString))
            {
                connection.Open();

                using (var reader = new MySqlCommand("SELECT * FROM History", connection).ExecuteReader())
                {
                    if (!reader.HasRows)
                        return;

                    while(reader.Read())
                    {
                        HistoryCollection.Add(new Hist() { Text = reader.GetString("Text") });
                    }
                }

                connection.Close();
            }
        }
    }
}
