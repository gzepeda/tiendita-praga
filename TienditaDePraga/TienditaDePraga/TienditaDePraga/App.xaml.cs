using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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
                    //database.CreateTables();
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                MainPage = new MainMenuPage();
            else
                MainPage = new MainMenuPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=8e8abc7f-c6f2-4d8d-a977-ff7e09c63541;" + 
                "uwp={Your UWP App secret here};" +
                   "android=006b282f-5986-480f-aaf4-c4d9cac40d7b",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}