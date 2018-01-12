using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(TienditaDePraga.DatabaseService))]
namespace TienditaDePraga
{
    public class DatabaseService
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            //Create all the tables
            database.CreateTableAsync<Mes>().Wait();
            database.CreateTableAsync<Cliente>().Wait();
            database.CreateTableAsync<Producto>().Wait();
            database.CreateTableAsync<Consumo>().Wait();
        }

        public Task<List<T>> GetAllItemsAsync<T> () where T: new()
        {
            return database.Table<T>().ToListAsync();
        }

        //public Task<List<Mes>> GetItemsAsync()
        //{
        //    return database.Table<Mes>().ToListAsync();
        //}

        public Task<List<Mes>> GetOrderedMesesAsync()
        {
            return database.QueryAsync<Mes>("SELECT * FROM [Mes] " +
                "ORDER BY Anio DESC, NumeroMes DESC");
        }

        //public Task<List<Mes>> GetItemsNotDoneAsync()
        //{
        //    return database.QueryAsync<Mes>("SELECT * FROM [Mes] WHERE [Anio] = 2017");
        //}

        public Task<List<Cliente>> GetClientesForMesAsync(Mes mes)
        {
            return database.QueryAsync<Cliente>($"SELECT * FROM [Cliente] " +
                $"WHERE [MesId] = '{mes.Id}'");
        }

        public Task<List<Consumo>> GetConsumosForClienteAsync(Cliente cliente)
        {
            return database.QueryAsync<Consumo>($"SELECT [Consumo].* " +
                $" FROM [Consumo] " +
                $" WHERE [MesId] = '{cliente.MesId}' " +
                $"  AND [ClienteId] = '{cliente.Id}'");
        }

        public async Task<List<Consumo>> InsertarConsumosDefaultAsync(Cliente cliente)
        {
            Consumo consumo;
            //Selecciono la lista de productos y los ingreso con consumo cero
            var productos = await GetAllItemsAsync<Producto>();
            if (productos.Count == 0)
            {
                productos = CargarProductosIniciales();
            }

            foreach (var producto in productos)
            {
                consumo = new Consumo
                {
                    MesId = cliente.MesId,
                    ClienteId = cliente.Id,
                    ProductoId = producto.Id,
                    CantidadConsumida = 0,
                    Id = Guid.NewGuid().ToString()
                };
                var result = await InsertItemAsync<Consumo>(consumo);

            }

            //Retorno la lista de consumos ingresados
            return await GetConsumosForClienteAsync(cliente);
        }

        private List<Producto> CargarProductosIniciales()
        {
            var items = new List<Producto>
            {
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Tortrix Varios", Descripcion = "Tortrix Varios", PrecioBase = 2 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Frituras tipo Lays", Descripcion = "Lays", PrecioBase = 4 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Coca Cola Lata", Descripcion = "Gaseosas", PrecioBase = 6},
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Del Monte", Descripcion = "Jugos", PrecioBase = 6 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Chocolates variados", Descripcion = "Snacks Chocolates",  PrecioBase = 11 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cerveza", Descripcion = "Cerveza", PrecioBase = 10 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Chips Ahoy", Descripcion = "Snacks",  PrecioBase = 7 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Chupetes", Descripcion = "Snacks",  PrecioBase = 5 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Magdalena", Descripcion = "Magdalena",  PrecioBase = 3 },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Dona", Descripcion = "Dona",  PrecioBase = 6 },
            };

            database.InsertAllAsync(items);

            return items;
        }

        public Task<Mes> GetItemAsync(string id)
        {
            return database.Table<Mes>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            return database.FindAsync<T>(predicate);
        }

        public Task<int> SaveItemAsync(Mes item)
        {
            if (!string.IsNullOrEmpty(item.Id))
            {
                return database.InsertOrReplaceAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> InsertItemAsync<T>(T item) where T:new()
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> UpdateItemAsync<T>(T item) where T:new()
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Mes item)
        {
            return database.DeleteAsync(item);
        }
    }
}
