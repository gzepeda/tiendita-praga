using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(TienditaDePraga.MockDataStore))]
namespace TienditaDePraga
{
    public class MockDataStore : IDataStore<Mes>
    {
        List<Mes> items;

        public MockDataStore()
        {
            items = new List<Mes>();
            var mockItems = new List<Mes>
            {
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Septiembre 2017", Anio = 2017, NumeroMes = 9 },
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Diciembre 2017", Anio = 2017, NumeroMes = 12 },
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Noviembre 2017", Anio = 2017, NumeroMes = 11},
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Octubre 2017", Anio = 2017, NumeroMes = 10 },
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Agosto 2017", Anio = 2017, NumeroMes = 8 },
                new Mes { Id = Guid.NewGuid().ToString(), Nombre = "Julio 2017", Anio = 2017, NumeroMes = 7 },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }

            items = items.OrderByDescending(item => item.Anio).ThenByDescending(item => item.NumeroMes).ToList();
        }

        public async Task<bool> AddItemAsync(Mes item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Mes item)
        {
            var _item = items.Where((Mes arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Mes arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Mes> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Mes>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
