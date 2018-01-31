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

            MessagingCenter.Subscribe<NewConsumoPage, Consumo>(this, "AgregarConsumo", async (obj, item) =>
            {
                MessagingCenter.Unsubscribe<NewConsumoPage, Consumo>(this, "AgregarConsumo");
                var _item = item as Consumo;

                var prod = await App.Database.Get<Producto>(p => p.Id == _item.ProductoId);

                var detalleConsumo = new DetalleConsumo();
                detalleConsumo.P = prod;
                detalleConsumo.C = _item;
                Consumos.Add(detalleConsumo);

                await App.Database.InsertItemAsync(_item);
            });
        }

        public async Task AgregarConsumo(Consumo c)
        {
            c.CantidadConsumida++;
            await App.Database.UpdateItemAsync(c);
        }

        public async Task QuitarConsumo(Consumo c)
        {
            if (c.CantidadConsumida > 0)
            {
                c.CantidadConsumida--;
                await App.Database.UpdateItemAsync(c);
            }
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
