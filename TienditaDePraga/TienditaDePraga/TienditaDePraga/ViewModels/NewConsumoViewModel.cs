using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public class NewConsumoViewModel : BaseViewModel
    {
        public IList<Producto> Productos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public NewConsumoViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Productos.Clear();
                Productos = await App.Database.GetAllItemsAsync<Producto>();
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
    }
}
