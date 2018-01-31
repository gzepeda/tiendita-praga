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
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Botonetas Granada", Descripcion = "Botonetas Granada", PrecioBase = 1, CostoUnitario = 0.41M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cerecitas Diana", Descripcion = "Cerecitas Diana", PrecioBase = 1, CostoUnitario = 0.42M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Elotitos Diana", Descripcion = "Elotitos Diana", PrecioBase = 1, CostoUnitario = 0.42M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Maiz Chino Diana", Descripcion = "Maiz Chino Diana", PrecioBase = 1, CostoUnitario = 0.42M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Palitos Diana", Descripcion = "Palitos Diana", PrecioBase = 1, CostoUnitario = 0.42M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Tortillitas", Descripcion = "Tortillitas", PrecioBase = 1, CostoUnitario = 0.42M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cebollitas", Descripcion = "Cebollitas", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Chicky", Descripcion = "Chicky", PrecioBase = 2, CostoUnitario = 1.21M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Crispin", Descripcion = "Crispin", PrecioBase = 2, CostoUnitario = 1M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Manías Japonés Diana", Descripcion = "Manías Japonés Diana", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Manías Limón Diana", Descripcion = "Manías Limón Diana", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Manías Saladas Diana", Descripcion = "Manías Saladas Diana", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Pikarones", Descripcion = "Pikarones", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Ricitos", Descripcion = "Ricitos", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Tortrix Barbacóa", Descripcion = "Tortrix Barbacóa", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Tortrix Limón", Descripcion = "Tortrix Limón", PrecioBase = 2, CostoUnitario = 0.83M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cheetos Crunch", Descripcion = "Cheetos Crunch", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cheetos Puff", Descripcion = "Cheetos Puff", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Crujitos", Descripcion = "Crujitos", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Doritos Naranja", Descripcion = "Doritos Naranja", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Doritos Rojos", Descripcion = "Doritos Rojos", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Fiesta Snack", Descripcion = "Fiesta Snack", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Lays", Descripcion = "Lays", PrecioBase = 4, CostoUnitario = 1.29M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Cocoa Treats", Descripcion = "Cocoa Treats", PrecioBase = 5, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Drizzle Treats", Descripcion = "Drizzle Treats", PrecioBase = 5, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "M&M's Treats", Descripcion = "M&M's Treats", PrecioBase = 5, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Coca Cola", Descripcion = "Coca Cola", PrecioBase = 6, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Donas", Descripcion = "Donas", PrecioBase = 6, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Jugos Manzana", Descripcion = "Jugos Manzana", PrecioBase = 6, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Jugos Melocotón", Descripcion = "Jugos Melocotón", PrecioBase = 6, CostoUnitario = 3M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Chocolate Granada Rojo", Descripcion = "Chocolate Granada Rojo", PrecioBase = 7, CostoUnitario = 3.91M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Gallo", Descripcion = "Gallo", PrecioBase = 10, CostoUnitario = 6M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "3 Musketeers", Descripcion = "3 Musketeers", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "BabeRuth", Descripcion = "BabeRuth", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Butterfinger Barra", Descripcion = "Butterfinger Barra", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Butterfinger Cups", Descripcion = "Butterfinger Cups", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "M&M's   ", Descripcion = "M&M's   ", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "M&M's   Peanuts", Descripcion = "M&M's   Peanuts", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Milkiway", Descripcion = "Milkiway", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Skittle", Descripcion = "Skittle", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Snikers", Descripcion = "Snikers", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Starburst", Descripcion = "Starburst", PrecioBase = 11, CostoUnitario = 5.27M },
                new Producto { Id = Guid.NewGuid().ToString(), Nombre = "Twix", Descripcion = "Twix", PrecioBase = 11, CostoUnitario = 5.27M },
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

        public Task<int> DeleteItemAsync<T>(T item) where T:new()
        {
            return database.DeleteAsync(item);
        }
    }
}
