using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Collections.Generic;

namespace LibraryApp
{
    public partial class App : Application
    {
        private static DatabaseService _databaseService;

        public static DatabaseService Database
        {
            get
            {
                if (_databaseService == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LibraryApp.db3");
                    _databaseService = new DatabaseService(dbPath);
                }
                return _databaseService;
            }
        }

        public App()
        {
            InitializeComponent();
            App.Database.ResetDatabaseAsync().Wait();


            // Create and populate the database
            var books = new List<Book>
            {
                new Book { ISBN = "9788381350013", Title = "Verity", Author = "Colleen Hoover", IsCheckedOut = false },
                new Book { ISBN = "9780593449554", Title = "Dreamland", Author = "Nicholas Sparks", IsCheckedOut = false },
                new Book { ISBN = "9780593158364", Title = "The Golden Enclaves", Author = "Naomi Novik", IsCheckedOut = false },
                new Book { ISBN = "9780593446065", Title = "Lucy By The Sea", Author = "Elizabeth Strout", IsCheckedOut = false }
            };

            foreach (var book in books)
            {
                App.Database.SaveBookAsync(book);
            }

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
