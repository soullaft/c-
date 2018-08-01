namespace Vesna.SkladModel
{
    /// <summary>
    /// Предмет на складе
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long? ID { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public long? Count { get; set; }

        /// <summary>
        /// Ценая
        /// </summary>
        public double? Money { get; set; }

        /// <summary>
        /// Изменить данные о товаре
        /// </summary>
        /// <param name="newItem">Товар с измененными данными</param>
        public void Update(Item newItem)
        {
            ID = newItem.ID ?? ID;
            Name = newItem.Name ?? Name;
            Count = newItem.Count ?? Count;
            Money = newItem.Money ?? Money;
        }
    }
}
