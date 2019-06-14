using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        public async void OnItemAddedClicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEntry
            {
                BindingContext = new ItemModel()
            });
        }

        public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ItemEntry
                {
                    BindingContext = e.SelectedItem as ItemModel

                });
            }
        }
    }
}