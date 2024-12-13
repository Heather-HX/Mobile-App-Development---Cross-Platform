using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LibraryApp
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsCheckedOut { get; set; }
        public string Borrower { get; set; }
    }
}