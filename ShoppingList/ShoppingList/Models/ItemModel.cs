using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ShoppingList.Models
{
    public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime Date { get; set; }

        public ItemModel() { }

        public ItemModel(string store)
        {
            StoreName = store;
        }
    }
}
