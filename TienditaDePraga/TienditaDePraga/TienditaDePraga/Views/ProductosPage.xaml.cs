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
    public partial class ProductosPage : ContentPage
    {
        ProductosViewModel viewModel;

        public ProductosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductosViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Producto;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            //await Navigation.PushAsync(new ClientesMesPage(item));

            // Manually deselect item
            //ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewProductoPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Productos.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
            if (mi == null)
                return;

            var producto = mi.BindingContext as Producto;
            if (producto == null)
                return;

            await viewModel.removeProducto(producto);
        }
    }
}
