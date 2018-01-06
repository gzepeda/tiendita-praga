using SQLite;
using System;

namespace TienditaDePraga
{
    public class Cliente
    {
        [PrimaryKey]
        public String Id { get; set; }
        public string Nombre { get; set; }
        [Indexed]
        public string MesId { get; set; }
    }
}
