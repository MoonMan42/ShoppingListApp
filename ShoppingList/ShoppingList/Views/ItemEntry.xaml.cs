using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemEntry : ContentPage
    {
        public ItemEntry()
        {
            InitializeComponent();
        }

        public async void OnSavedButtonClicked(object sender, EventArgs e)
        {
            var item = (ItemModel)BindingContext;

            item.Date = DateTime.UtcNow;
            await App.Database.SaveItemAsync(item);
            await Navigation.PopAsync();
        }

        public async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var item = (ItemModel)BindingContext;

            await App.Database.DeleteItemAsyn(item);
            await Navigation.PopAsync();
        }
    }
}