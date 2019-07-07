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
            //_database.QueryAsync<ItemModel>("DELETE FROM [ItemModel]");  //Deletes database (For testing only)
        }

        /// <summary>
        /// Get a list of stores saved in DB
        /// </summary>
        /// <returns></returns>
        public Task<List<ItemModel>> GetStoreNameAsync()
        {
            return _database.QueryAsync<ItemModel>("SELECT * FROM [ItemModel] GROUP BY [StoreName] ");
        }

        /// <summary>
        /// get list of items 
        /// </summary>
        /// <returns></returns>
        public Task<List<ItemModel>> GetItemsAsync()
        {
            return _database.QueryAsync<ItemModel>("SELECT * FROM [ItemModel] ORDER BY [StoreName] ASC");
        }

        public Task<List<ItemModel>> GetItemsByStoreAsync(string storeName)
        {
            storeName = storeName.Replace("'", "''"); // check for apostrophe
            return _database.QueryAsync<ItemModel>($"SELECT * FROM [ItemModel] WHERE [StoreName] = \'{storeName}\' ORDER BY [StoreName] ASC");
        }

        /// <summary>
        /// Save individual items
        /// </summary>
        public Task<List<ItemModel>> SaveItemSelectedAsync(int id, int selected)
        {
            return _database.QueryAsync<ItemModel>($"UPDATE [ItemModel] SET [isSelected] = \'{selected}\' WHERE [Id] = {id}");
        }

        /// <summary>
        /// get item info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> GetItemAsync(int id)
        {
            return _database.Table<ItemModel>().Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Save item to database
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// Delete item from database 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Task<int> DeleteItemAsyn(ItemModel i)
        {
            return _database.DeleteAsync(i);
        }

        // Remove Greyed out options. 
        public Task<List<ItemModel>> DeleteSelectedItems(string store)
        {
            store = store.Replace("'", "''"); // check for apostrophe
            return _database.QueryAsync<ItemModel>($"DELETE FROM [ItemModel] WHERE [StoreName] = \'{store}\' AND [SelectedColor] = \"Gray\""); 
        }
    }
}
