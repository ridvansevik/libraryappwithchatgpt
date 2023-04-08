using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryappwithchatgpt
{
  
    class Program
    {
        static void Main(string[] args)
        {
            Functions library = new Functions();

            while (true)
            {
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Kitap Sil");
                Console.WriteLine("3. Kitap Ara");
                Console.WriteLine("4. Kitap Ödünç Al");
                Console.WriteLine("5. Kitap İade Et");
                Console.WriteLine("6. Programdan Çık");

                Console.WriteLine("7. Listele");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Eklemek istediğiniz kitabın adını girin:");
                        string title = Console.ReadLine();

                        Console.WriteLine("Kitabın yazarını girin:");
                        string author = Console.ReadLine();

                        Console.WriteLine("Kitabın yayıncısını girin:");
                        string publisher = Console.ReadLine();

                        Book book = new Book(title, author, publisher);
                        library.AddBook(book);

                        Console.WriteLine("Kitap başarıyla eklendi.");
                        break;

                    case 2:
                        Console.WriteLine("Silmek istediğiniz kitabın adını girin:");
                        string titleToDelete = Console.ReadLine();

                        List<Book> booksToDelete = library.SearchBooks(titleToDelete);

                        if (booksToDelete.Count == 0)
                        {
                            Console.WriteLine("Aradığınız isimde bir kitap bulunamadı.");
                        }
                        else
                        {
                            Console.WriteLine("Aşağıdaki kitapları silmek istiyor musunuz?");
                            foreach (Book bookToDelete in booksToDelete)
                            {
                                Console.WriteLine("{0} - {1} ({2})", bookToDelete.Title, bookToDelete.Author, bookToDelete.Publisher);
                            }

                            Console.WriteLine("Emin misiniz? (E/H)");
                            string confirmDelete = Console.ReadLine().ToLower();

                            if (confirmDelete == "e")
                            {
                                foreach (Book bookToDelete in booksToDelete)
                                {
                                    library.DeleteBook(bookToDelete);
                                    Console.WriteLine("{0} başarıyla silindi.", bookToDelete.Title);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Silme işlemi iptal edildi.");
                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("Kitap arama terimini girin:");
                        string searchTerm = Console.ReadLine();

                        List<Book> searchResults = library.SearchBooks(searchTerm);

                        if (searchResults.Count == 0)
                        {
                            Console.WriteLine("Aradığınız kelimeye uygun bir kitap bulunamadı.");
                        }
                        else
                        {
                            Console.WriteLine("Aşağıdaki kitaplar bulundu:");
                            foreach (Book result in searchResults)
                            {
                                Console.WriteLine("{0} - {1} ({2})", result.Title, result.Author, result.Publisher);
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Ödünç almak istediğiniz kitabın adını girin:");
                        string titleToBorrow = Console.ReadLine();

                        List<Book> booksToBorrow = library.SearchBooks(titleToBorrow);

                        if (booksToBorrow.Count == 0)
                        {
                            Console.WriteLine("Aradığınız isimde bir kitap bulunamadı.");
                        }
                        else
                        {
                            Console.WriteLine("Aşağıdaki kitapları ödünç almak istiyor musunuz?");
                            foreach (Book bookToBorrow in booksToBorrow)
                            {
                                Console.WriteLine("{0} - {1} ({2})", bookToBorrow.Title, bookToBorrow.Author, bookToBorrow.Publisher);
                            }

                            Console.WriteLine("Emin misiniz? (E/H)");
                            string confirmBorrow = Console.ReadLine().ToLower();

                            if (confirmBorrow == "e")
                            {
                                Console.WriteLine("Ödünç alacak kişinin adını girin:");
                                string borrowerName = Console.ReadLine();
                                foreach (Book bookToBorrow in booksToBorrow)
                                {
                                    bool success = library.BorrowBook(bookToBorrow, borrowerName);
                                    if (success)
                                    {
                                        Console.WriteLine("{0} başarıyla ödünç alındı.", bookToBorrow.Title);
                                    }
                                    else
                                    {
                                        Console.WriteLine("{0} ödünç alınamadı. Başka biri ödünç almış olabilir.", bookToBorrow.Title);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ödünç alma işlemi iptal edildi.");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;

                    case 6:
                        library.SaveLibrary();
                        Console.WriteLine("Programdan çıkılıyor.");
                        return;

                    case 7:
                        library.ListBooks();
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }

            }
        }
    }
}