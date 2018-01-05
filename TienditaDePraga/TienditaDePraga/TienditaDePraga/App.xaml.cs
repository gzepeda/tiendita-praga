using System;
using TienditaDePraga.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TienditaDePraga
{
    public partial class App : Application
    {
        static DatabaseService database;

        public static DatabaseService Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseService(DependencyService.Get<IFileHelper>().GetLocalFilePath("tienditaSQLite.db3"));
                    database.CreateTables();
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());
        }
    }
}