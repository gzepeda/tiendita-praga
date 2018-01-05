using System;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Mes
            {
                //TODO - Name of the month
                Nombre = DateTime.UtcNow.Month.ToString(),
                Anio = DateTime.UtcNow.Year, 
                NumeroMes = DateTime.UtcNow.Month
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
