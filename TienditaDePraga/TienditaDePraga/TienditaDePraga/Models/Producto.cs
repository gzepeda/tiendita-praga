using SQLite;
using System;

namespace TienditaDePraga
{
    public class Producto
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal CostoUnitario { get; set; }
    }
}
