using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryappwithchatgpt
{

    [Serializable]
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Book(string title, string author, string publisher)
        {
            Title = title;
            Author = author;
            Publisher = publisher;
        }
    }

    class BorrowedBook
    {
        public Book Book { get; set; }
        public string BorrowerName { get; set; }
        public DateTime BorrowDate { get; set; }

        public BorrowedBook(Book book, string borrowerName, DateTime borrowDate)
        {
            Book = book;
            BorrowerName = borrowerName;

            BorrowDate = borrowDate;
        }
    }
}
