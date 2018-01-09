using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public class ConsumoClientesViewModel : BaseViewModel
    {
        public ObservableCollection<DetalleConsumo> Consumos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Cliente ClienteSeleccionado { get; set; }

        public ConsumoClientesViewModel(Cliente cliente)
        {
            ClienteSeleccionado = cliente;
            Title = $"Consumos del Cliente: {ClienteSeleccionado.Nombre}";
            Consumos = new ObservableCollection<DetalleConsumo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            //TODO Ajustar para nuevos consumos
            //MessagingCenter.Subscribe<NewClientePage, Cliente>(this, "AgregarConsumo", async (obj, item) =>
            //{
            //    MessagingCenter.Unsubscribe<NewClientePage, Cliente>(this, "AgregarConsumo");
            //    var _item = item as Cliente;
            //    Consumos.Add(_item);
            //    await App.Database.InsertItemAsync<Cliente>(_item);
            //});
        }

        public async Task AgregarConsumo(Consumo c)
        {
            c.CantidadConsumida++;
            await App.Database.UpdateItemAsync<Consumo>(c);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Consumos.Clear();
                var dbItems = await App.Database.GetConsumosForClienteAsync(ClienteSeleccionado);

                if (dbItems.Count == 0)
                {
                    //Si no hay consumos, inserto todos los productos con consumo
                    // cero por defecto
                    dbItems = await App.Database.InsertarConsumosDefaultAsync(ClienteSeleccionado);
                }

                //Inserto consumos con su respectivo producto en la estructura
                // para poder hacer el binding y mostrar nombres en pantalla
                foreach (var item in dbItems)
                {
                    var prod = await App.Database.Get<Producto>(p => p.Id == item.ProductoId);

                    var detalleConsumo = new DetalleConsumo();
                    detalleConsumo.P = prod;
                    detalleConsumo.C = item;
                    Consumos.Add(detalleConsumo);
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
