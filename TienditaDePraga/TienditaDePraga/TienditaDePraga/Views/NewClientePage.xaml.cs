using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class NewClientePage : ContentPage
    {
        public Cliente ClienteNuevo { get; set; }

        public NewClientePage(Mes mes)
        {
            InitializeComponent();

            ClienteNuevo = new Cliente
            {
                //Nombre = "Ingrese el nombre del cliente",
                Nombre = "",
                MesId = mes.Id,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AgregarCliente", ClienteNuevo);
            //await Navigation.PopToRootAsync();
            await Navigation.PopAsync();
        }
    }
}
