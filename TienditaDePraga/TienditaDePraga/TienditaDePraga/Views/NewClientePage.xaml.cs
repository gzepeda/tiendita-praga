using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class NewClientePage : ContentPage
    {
        public Cliente Item { get; set; }

        public NewClientePage()
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
