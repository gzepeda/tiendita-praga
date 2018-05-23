using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TienditaDePraga
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewProductoPage : ContentPage
	{
        public Producto Item { get; set; }

		public NewProductoPage ()
		{
            InitializeComponent();

            Item = new Producto
            {
                //TODO - Name of the month
                PrecioBase = 2,
                CostoUnitario = 1,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}