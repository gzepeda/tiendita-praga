using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TienditaDePraga
{
	public class ProductosViewModel : BaseViewModel
    {

        public ObservableCollection<Producto> Productos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ProductosViewModel ()
		{
            Title = "Productos";
            Productos = new ObservableCollection<Producto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewProductoPage, Producto>(this, "AddItem", async (obj, item) =>
            {
                MessagingCenter.Unsubscribe<NewProductoPage, Producto>(this, "AddItem");
                var _item = item as Producto;
                Productos.Add(_item);
                await App.Database.InsertItemAsync<Producto>(_item);
                //await DataStore.AddItemAsync(_item);
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Productos.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //Test reading from the DB
                //var dbItems = await App.Database.GetOrderedMesesAsync();
                var dbItems = await App.Database.GetAllItemsAsync<Producto>();

                foreach (var item in dbItems)
                {
                    Productos.Add(item);
                    //Test saving in DB
                    //var result = await App.Database.SaveItemAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task removeProducto(Producto producto)
        {
            try
            {
                await App.Database.DeleteItemAsync<Producto>(producto);
                Productos.Remove(producto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}