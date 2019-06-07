using ShoppingList.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Database
{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ItemModel>().Wait();
        }

        public Task<List<ItemModel>> GetItemsAsync()
        {
            return _database.Table<ItemModel>().ToListAsync();
        } 

        public Task<ItemModel> GetItemAsync(int id)
        {
            return _database.Table<ItemModel>().Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ItemModel item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsyn(ItemModel item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
