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
            return _database.QueryAsync<ItemModel>("SELECT * FROM [ItemModel] ORDER BY [StoreName] ASC");
        } 

        public Task<ItemModel> GetItemAsync(int id)
        {
            return _database.Table<ItemModel>().Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ItemModel i)
        {
            if (i.Id != 0)
            {
                return _database.UpdateAsync(i);
            }
            else
            {
                return _database.InsertAsync(i);
            }
        }

        public Task<int> DeleteItemAsyn(ItemModel i)
        {
            return _database.DeleteAsync(i);
        }
    }
}
