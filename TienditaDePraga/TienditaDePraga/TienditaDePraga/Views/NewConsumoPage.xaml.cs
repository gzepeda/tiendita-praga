using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class NewConsumoPage : ContentPage
    {
        NewConsumoViewModel viewModel;
        public Consumo ConsumoNuevo { get; set; }
        public Producto ProductoSeleccionado { get; set; }

        public NewConsumoPage(Cliente cliente)
        {
            InitializeComponent();

            ConsumoNuevo = new Consumo
            {
                //Nombre = "Ingrese el nombre del cliente",
                //Nombre = "",
                CantidadConsumida = 0,
                ClienteId = cliente.Id,
                MesId = cliente.MesId,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = viewModel = new NewConsumoViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Productos == null)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (ProductoSeleccionado == null)
                return;

            ConsumoNuevo.ProductoId = ProductoSeleccionado.Id;
            MessagingCenter.Send(this, "AgregarConsumo", ConsumoNuevo);
            await Navigation.PopAsync();
        }
    }
}
