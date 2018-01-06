using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using TienditaDePraga.Services;

namespace TienditaDePraga
{
    public class ClienteMesViewModel : BaseViewModel
    {
        public ObservableCollection<Cliente> Clientes { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Mes MesSeleccionado { get; set; }

        public ClienteMesViewModel(Mes mes)
        {
            Title = "Clientes del Mes";
            MesSeleccionado = mes;
            Clientes = new ObservableCollection<Cliente>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
                        
            MessagingCenter.Subscribe<NewClientePage, Cliente>(this, "AgregarCliente", async (obj, item) =>
            {
                MessagingCenter.Unsubscribe<NewClientePage, Cliente>(this, "AgregarCliente");
                var _item = item as Cliente;
                Clientes.Add(_item);
                await App.Database.InsertItemAsync<Cliente>(_item);
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
                Clientes.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //Test reading from the DB
                var dbItems = await App.Database.GetClientesForMesAsync(MesSeleccionado);
                                
                foreach (var item in dbItems)
                {
                    Clientes.Add(item);
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
    }
}
