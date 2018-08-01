namespace Vesna
{
    /// <summary>
    /// Класс содержащий данные о подключении к базе данных
    /// </summary>
    public sealed class MyDB
    {
        #region Public Fields
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public static string ConnectionString { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор типа
        /// </summary>
        static MyDB()
        {
            ConnectionString = $"server={Properties.Settings.Default.server};database={Properties.Settings.Default.database};user={Properties.Settings.Default.user};password={Properties.Settings.Default.password};charset=utf8;SslMode=none";
        }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public MyDB(){ }

        #endregion

        #region Helpers

        /// <summary>
        /// Обновить данные о подключении
        /// </summary>
        public static void UpdateConnection()
        {
            ConnectionString = $"server={Properties.Settings.Default.server};database={Properties.Settings.Default.database};user={Properties.Settings.Default.user};password={Properties.Settings.Default.password}; SslMode=none";
        }

        #endregion
    }
}
