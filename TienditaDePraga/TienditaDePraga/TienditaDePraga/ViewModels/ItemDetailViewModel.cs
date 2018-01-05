using System;

namespace TienditaDePraga
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Mes Item { get; set; }
        public ItemDetailViewModel(Mes item = null)
        {
            Title = item?.Nombre;
            Item = item;
        }
    }
}
