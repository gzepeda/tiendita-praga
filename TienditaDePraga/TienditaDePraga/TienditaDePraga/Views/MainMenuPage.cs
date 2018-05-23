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
            // create an array of the Page names
            string[] myPageNames = {
                "Main",
                "Page 2",
                "Page 3",
            };

            // Create ListView for the Master page.
            ListView listView = new ListView
            {
                ItemsSource = myPageNames,
            };

            Page itemsPage, aboutPage = null;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new MesPage())
                    {
                        Title = "Browse"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    itemsPage = new NavigationPage(new MesPage())
                    {
                        Title = "Browse"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    break;
            }

            // The Master page is actually the Menu page for us
            this.Master = new ContentPage
            {
                Title = "The Title is required.",
                BackgroundColor = Color.Silver,
                Content = new StackLayout
                {
                    Padding = new Thickness(5, 50),
                    Children = { Link(this, "Meses", itemsPage),
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
