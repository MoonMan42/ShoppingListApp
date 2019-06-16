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
    public partial class StorePage : ContentPage
    {
        public StorePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            storeListView.ItemsSource = await App.Database.GetStoreNameAsync();
        }

        public async void OnItemAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEntry
            {
                BindingContext = new ItemModel()
            });
        }

        public async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ItemModel)e.SelectedItem;

            //await DisplayAlert("Test", "" + item.StoreName, "Ok"); // alert dialog box

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ItemsPage(item.StoreName)
                {
                    BindingContext = e.SelectedItem as ItemModel

                });
            }
        }

    }
}