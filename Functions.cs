using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryappwithchatgpt
{
    public class Functions
    {

        private List<Book> books = new List<Book>();
        private List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();

        public void Library()
        {
            string filePath = "library.dat";

            if (!File.Exists(filePath))
            {
                // Create an empty file if it does not exist
                using (FileStream fs = File.Create(filePath))
                {
                    // Do nothing
                }
            }

            // Load books from the file
            using (Stream stream = File.OpenRead(filePath))
            {
                if (stream.Length != 0) // Add null check
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    books = (List<Book>)binaryFormatter.Deserialize(stream);
                }
            }
        }


        public void AddBook(Book book)
        {
            books.Add(book);
            save();
        }
        public void save()
        {
            string filePath = "library.dat";

            using (Stream stream = File.OpenWrite(filePath))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, books);
            }
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            // Return a list of all books that match the search term
            return books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                                    b.Author.ToLower().Contains(searchTerm.ToLower()) ||
                                    b.Publisher.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public bool BorrowBook(Book book, string borrowerName)
        {
            if (book.IsAvailable)
            {
                book.IsAvailable = false;
                borrowedBooks.Add(new BorrowedBook(book, borrowerName, DateTime.Now));
                Console.WriteLine("Kitap başarıyla ödünç alındı.");
                return true;
            }
            else
            {
                Console.WriteLine("Üzgünüz, bu kitap şu anda müsait değil.");
                return false;
            }
        }

        public void DeleteBook(Book book)
        {
            books.Remove(book);
            save();
        }

        public void ListBooks()
    {
            foreach (var book in books)
            {
                string borrowedMark = "";
                if (!book.IsAvailable)
                {
                    borrowedMark = " (Ödünç Alındı)";
                }
                Console.WriteLine($"{book.Title} - {book.Author}{borrowedMark}");
            }
            Console.ReadKey();
        }

        public void ListBorrowedBooks()
        {
            Console.WriteLine("Ödünç Alınmış Kitaplar:");
            foreach (var borrowedBook in borrowedBooks)
            {
                Console.WriteLine($"{borrowedBook.Book.Title} - {borrowedBook.Book.Author} - {borrowedBook.BorrowerName} - {borrowedBook.BorrowDate}");
            }
            Console.ReadKey();
        }


        public void ReturnBook(Book book)
        {
            // Find the borrowed book that matches the given book
            var borrowedBook = borrowedBooks.FirstOrDefault(bb => bb.Book == book);

            if (borrowedBook == null)
            {
                Console.WriteLine("Bu kitap ödünç alınmamış.");
                return;
            }

            // Mark the book as available
            book.IsAvailable = true;

            // Remove the borrowed book from the borrowedBooks list
            borrowedBooks.Remove(borrowedBook);

            Console.WriteLine("Kitap başarıyla iade edildi.");
        }



        public void SaveLibrary()
        {
            // Serialize the books list and save it to a file
            string filePath = "library.dat";

            try
            {
                using (Stream stream = File.OpenWrite(filePath))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, books);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving library: {0}", ex.Message);
            }
        }
    }
}
