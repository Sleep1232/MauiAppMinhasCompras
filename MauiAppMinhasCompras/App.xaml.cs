using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db; // propriedade

        public static SQLiteDatabaseHelper Db // campo
        {
            get
            {
                if(_db==null)
                {
                    string cam_db3 = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras_db3"
                        ); // especifica o local do banco de dados

                    _db = new SQLiteDatabaseHelper(cam_db3);
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
