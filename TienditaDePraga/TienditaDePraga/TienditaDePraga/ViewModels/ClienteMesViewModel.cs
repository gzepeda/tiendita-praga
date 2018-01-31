using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Messaging;

namespace TienditaDePraga
{
    public class ClienteMesViewModel : BaseViewModel
    {
        private readonly string tabCharacter = " ==> ";
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

        internal Task EnviarReporte(string reporte, string correo)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                //emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

                try
                {
                    // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
                    var email = new EmailMessageBuilder()
                      .To(correo)
                      //  .Cc("cc.plugins@xamarin.com")
                      //  .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                      .Subject($"Reporte Tiendita del Praga para el mes de {MesSeleccionado.Nombre}")
                      .Body($"Adjunto --> Reporte Tiendita del Praga para el mes de {MesSeleccionado.Nombre}")
                      .WithAttachment(reporte, "text/plain")
                      .Build();

                    emailMessenger.SendEmail(email);
                } catch(Exception ex)
                {
                    //TODO - Show alert
                    Debug.WriteLine(ex.Message);
                }
            }
            return Task.FromResult(true);
        }

        public async Task removeCliente(Cliente cliente)
        {
            try
            {
                await App.Database.DeleteItemAsync<Cliente>(cliente);
                Clientes.Remove(cliente);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<string> GenerarReporteAsync()
        {
            FileStorage file = new FileStorage(MesSeleccionado.Nombre.Replace(@"/", string.Empty) + ".txt");
            //String linea = $"Reporte del mes de {MesSeleccionado.Nombre}\r\n";
            String linea = "";
            linea = linea + "Cliente" + tabCharacter + "ConsumoDelMes" + Environment.NewLine;
            //Recorro los Consumos y genero un archivo txt al final. 
            foreach (var cliente in Clientes)
            {
                linea = linea + cliente.Nombre + tabCharacter;

                var consumos = await App.Database.GetConsumosForClienteAsync(cliente);
                Producto producto;
                decimal totalConsumos = 0;
                foreach (var consumo in consumos)
                {
                    producto = await App.Database.Get<Producto>(p => p.Id == consumo.ProductoId);
                    totalConsumos = totalConsumos + (consumo.CantidadConsumida * producto.PrecioBase);
                }
                linea =  linea +  totalConsumos.ToString() + Environment.NewLine;
                Debug.WriteLine(linea);
            }
            await file.AddStringToReport(linea);
            return file.FilePath;
        }
    }
}
