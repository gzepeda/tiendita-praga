using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class DetalleClientePage : ContentPage
    {
        public Cliente Item { get; set; }

        public DetalleClientePage()
        {
            InitializeComponent();

            Item = new Cliente
            {
                Nombre = "Ingrese nombre de cliente",
                Id = new Guid().ToString()
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
