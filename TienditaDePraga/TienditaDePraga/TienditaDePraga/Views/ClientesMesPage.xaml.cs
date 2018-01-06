using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TienditaDePraga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesMesPage : ContentPage
    {
        ClienteMesViewModel viewModel;
        private Mes MesActual { get; set; }

        public ClientesMesPage(Mes mes)
        {
            InitializeComponent();

            MesActual = mes;

            BindingContext = viewModel = new ClienteMesViewModel(MesActual);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewClientePage(MesActual));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Clientes.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
