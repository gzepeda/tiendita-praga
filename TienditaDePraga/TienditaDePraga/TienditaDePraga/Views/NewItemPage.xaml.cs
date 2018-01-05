using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class NewItemPage : ContentPage
    {
        public Mes Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Mes
            {
                //TODO - Name of the month
                Nombre = DateTime.UtcNow.Month.ToString(),
                Anio = DateTime.UtcNow.Year,
                NumeroMes = DateTime.UtcNow.Month
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
