﻿using System;
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Cliente;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            await Navigation.PushAsync(new ConsumoClientesPage(item));

            // Manually deselect item
            ClientesMesListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewClientePage(MesActual));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.SuscribirAMensajes();

            if (viewModel.Clientes.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void EnviarReporte_Clicked(object sender, EventArgs e)
        {
            //Generar Reporte
            String reporte = await viewModel.GenerarReporteAsync();

            //Pedir Correo
            //String email = await VisualUtils.InputBox(this.Navigation);

            //Enviar por correo
            await viewModel.EnviarReporte(reporte, "");
        }
        async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
            if (mi == null)
                return;

            var cliente = mi.BindingContext as Cliente;
            if (cliente == null)
                return;

            await viewModel.removeCliente(cliente);
        }
    }
}
