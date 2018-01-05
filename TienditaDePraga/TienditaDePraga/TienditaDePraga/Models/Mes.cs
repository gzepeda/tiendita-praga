using SQLite;
using System;

namespace TienditaDePraga
{
    public class Mes
    {
        [PrimaryKey]
        public String Id { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public int NumeroMes { get; set; }
    }
}
