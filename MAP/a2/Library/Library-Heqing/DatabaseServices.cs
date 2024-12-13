using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Book>().Wait();
        }

        public Task<List<Book>> GetBooksAsync()
        {
            return _database.Table<Book>().ToListAsync();
        }

        public Task<int> SaveBookAsync(Book book)
        {
            if (book.Id != 0)
                return _database.UpdateAsync(book);
            else
                return _database.InsertAsync(book);
        }

        public Task<Book> GetBookByIdAsync(int id)
        {
            return _database.Table<Book>().Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteBookAsync(Book book)
        {
            return _database.DeleteAsync(book);
        }

        public Task ResetDatabaseAsync()
        {
            return _database.DropTableAsync<Book>().ContinueWith(t => _database.CreateTableAsync<Book>());
        }
    }
}
