using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class MesPage : ContentPage
    {
        MesViewModel viewModel;

        public MesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Mes;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            await Navigation.PushAsync(new ClientesMesPage(item));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMesPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Meses.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}