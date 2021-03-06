﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TienditaDePraga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
        async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
            if (mi == null)
                return;

            var mes = mi.BindingContext as Mes;
            if (mes == null)
                return;

            await viewModel.removeMes(mes);
        }

    }
}