using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public class MesViewModel : BaseViewModel
    {
        public ObservableCollection<Mes> Meses { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MesViewModel()
        {
            Title = "Browse";
            Meses = new ObservableCollection<Mes>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewMesPage, Mes>(this, "AddItem", async (obj, item) =>
            {
                MessagingCenter.Unsubscribe<NewMesPage, Mes>(this, "AddItem");
                var _item = item as Mes;
                Meses.Add(_item);
                await App.Database.SaveItemAsync(_item);
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
                Meses.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //Test reading from the DB
                var dbItems = await App.Database.GetOrderedMesesAsync();
                //var dbItems = await App.Database.GetAllItemsAsync<Mes>();
                                
                foreach (var item in dbItems)
                {
                    Meses.Add(item);
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
        public async Task removeMes(Mes mes)
        {
            try
            {
                await App.Database.DeleteItemAsync<Mes>(mes);
                Meses.Remove(mes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
