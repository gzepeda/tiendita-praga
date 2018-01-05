using SQLite;
using System;
using System.Collections.Generic;

namespace TienditaDePraga
{
    public class Consumo
    {
        [PrimaryKey]
        public string Id { get; set; }
        [Indexed]
        public string ClienteId { get; set; }
        [Indexed]
        public string MesId { get; set; }
        [Indexed]
        public string ProductoId { get; set; }
        public int CantidadConsumida { get; set; }
    }
}
