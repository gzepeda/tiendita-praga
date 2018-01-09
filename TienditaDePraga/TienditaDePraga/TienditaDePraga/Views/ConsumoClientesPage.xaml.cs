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
    public partial class ConsumoClientesPage : ContentPage
    {
        ConsumoClientesViewModel viewModel;
        private Cliente ClienteActual { get; set; }

        public ConsumoClientesPage(Cliente cliente)
        {
            InitializeComponent();

            ClienteActual = cliente;

            BindingContext = viewModel = new ConsumoClientesViewModel(ClienteActual);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DetalleConsumo;
            if (item == null)
                return;

            //Guardar consumo seleccionado
            await viewModel.AgregarConsumo(item.C);

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            await Navigation.PopAsync();

            // Manually deselect item
            ConsumoClientesListView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Consumos.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
