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
        public DateTime Date { get; set; }
    }
}
