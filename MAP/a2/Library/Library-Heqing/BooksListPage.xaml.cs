using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp
{
    public partial class BooksListPage : ContentPage
    {
        private readonly string _loggedInUser;
        private Book _selectedBook;  // Track the selected book

        public BooksListPage(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            WelcomeLabel.Text = $"Welcome, {loggedInUser}";
        }

        // Load the books from the database when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var books = await App.Database.GetBooksAsync();  // Fetch books from SQLite DB
            BooksListView.ItemsSource = books;  // Bind books to the ListView
        }

        // Handle book selection and update the selected book state
        private void OnBookSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedBook = (Book)e.SelectedItem;  
        }

        // Handle the Checkout button click (always enabled)
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Check if a book is selected and can be checked out
            if (_selectedBook == null)
            {
                await DisplayAlert("Error", "No book selected. Please select a book first.", "OK");
                return;
            }

            if (_selectedBook.IsCheckedOut)
            {
                await DisplayAlert("Error", "The book is already checked out.", "OK");
                return;
            }

            // Proceed to check out the book
            _selectedBook.IsCheckedOut = true;
            _selectedBook.Borrower = _loggedInUser;

            // Save changes to the database
            await App.Database.SaveBookAsync(_selectedBook);
            await DisplayAlert("Success", "You have successfully checked out the book.", "OK");

            // Clear the selected item and reload the list
            BooksListView.SelectedItem = null; 
            _selectedBook = null;  
            OnAppearing();  // Reload the list
        }

        // Handle the Return button click (always enabled)
        private async void OnReturnClicked(object sender, EventArgs e)
        {
            // Check if a book is selected and can be returned
            if (_selectedBook == null)
            {
                await DisplayAlert("Error", "No book selected. Please select a book first.", "OK");
                return;
            }

            if (_selectedBook.Borrower != _loggedInUser)
            {
                await DisplayAlert("Error", "You can only return books that you borrowed.", "OK");
                return;
            }

            // Proceed to return the book
            _selectedBook.IsCheckedOut = false;
            _selectedBook.Borrower = string.Empty;

            // Save changes to the database
            await App.Database.SaveBookAsync(_selectedBook);
            await DisplayAlert("Success", "You have successfully returned the book.", "OK");

            // Clear the selected item and reload the list
            BooksListView.SelectedItem = null;  
            _selectedBook = null;  
            OnAppearing();  
        }
    }
}
