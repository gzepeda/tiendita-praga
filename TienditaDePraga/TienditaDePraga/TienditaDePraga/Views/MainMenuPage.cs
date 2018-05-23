using System;

using Xamarin.Forms;

namespace TienditaDePraga
{
    public class MainMenuPage : MasterDetailPage
    {
        public MainMenuPage()
        {
            // Set up the Master, i.e. the Menu

            Label header = new Label
            {
                Text = "MENU",
                Font = Font.SystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };

            Page itemsPage = new NavigationPage(new MesPage())
            {
                Title = "Cortes"
            };

            Page aboutPage = new NavigationPage(new AboutPage())
            {
                Title = "Acerca de"
            };

            Page productosPage = new NavigationPage(new ProductosPage())
            {
                Title = "Lista de productos"
            };

            // The Master page is actually the Menu page for us
            this.Master = new ContentPage
            {
                Title = "ΞΞ",
                BackgroundColor = Color.Silver,
                Content = new StackLayout
                {
                    Padding = new Thickness(5, 50),
                    Children = { Link(this, "Cortes", itemsPage),
                                 Link(this, "Productos", productosPage),
                                 Link(this, "Acerca de...", aboutPage)}
                }
            };

            // Set up the Detail, i.e the Home or Main page.
            this.Detail = itemsPage;
        }

        static Button Link(MasterDetailPage mdPage, string name, Page page)
        {
            var button = new Button
            {
                Text = name,
                BackgroundColor = Color.FromRgb(0.9, 0.9, 0.9)
            };
            button.Clicked += delegate {
                mdPage.Detail = page;
                mdPage.IsPresented = false;
            };
            return button;
        }
    }
}
