using SQLite;
using System;

namespace TienditaDePraga
{
    public class Cliente
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Nombre { get; set; }
        [Indexed]
        public string MesId { get; set; }
    }
}
