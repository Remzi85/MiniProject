using Project___ConsoleApp__Library_Management_Application_.Models;
using Project___ConsoleApp__Library_Management_Application_.Services.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            IAuthorService authorServices = new AuthorService();
            IBorrowerService borrowerService = new BorrowerService();
            ILoanService loanService = new LoanService();
            ILoanItemService loanItemService = new LoanItemService();

            while (true)
            {
                Console.WriteLine("  \nLibrary Management Application");
                Console.WriteLine("  1 - Author actions");
                Console.WriteLine("  2 - Book actions");
                Console.WriteLine("  3 - Borrower actions");
                Console.WriteLine("  4 - BorrowBook");
                Console.WriteLine("  5 - ReturnBook ");
                Console.WriteLine("  6 - En cox borrow olunan kitab");
                Console.WriteLine("  7 - Kitabi gecikdiren Borrowerlerin listi");
                Console.WriteLine("  8 - Hansi borrower indiye qeder borrow olunan kitablar");
                Console.WriteLine("  9 - FilterBooksByTitle");
                Console.WriteLine(" 10 - FilterBooksByAuthor");
                Console.WriteLine("  0-Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AuthorAction(authorServices); break;
                    case "2": BookAction(bookService); break;
                    case "3": BorrowerAction(borrowerService); break;
                    case "6": MostBorrowedBook(loanService); break;
                    case "9":
                        Console.WriteLine("Enter book title");
                        string? title = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Title connot be empty");
                            break;
                        }
                        var filterBook = FilterBooksByTitle(bookService, title.Trim());
                        break;
                }
            }
        }

        private static void FilterBooksByAuthor(IBookService bookService, string? v, string? authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentNullException();
            }
            var books = bookService.GetAll();
        }

        static List<Book> FilterBooksByTitle(IBookService bookService, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            var filteredBooks = bookService.GetAll()?.Where(x => !string.IsNullOrWhiteSpace(x.Title) &&
                                                                  x.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                                                                  .ToList() ?? new List<Book>();

            if (!filteredBooks.Any())
                Console.WriteLine("No books found");

            return filteredBooks;
        }



        private static void MostBorrowedBook(ILoanService loanService)
        {
            var mostBorrowed = loanService.GetAll()?
       .SelectMany(x => x.LoanItems)
       .GroupBy(x => x.BookId)
       .OrderByDescending(x => x.Count())
       .FirstOrDefault();

            if (mostBorrowed == null)
            {
                Console.WriteLine("No books have been borrowed");
                return;
            }

            var book = mostBorrowed.FirstOrDefault()?.Book;
            Console.WriteLine(book != null
                ? $"Most borrowed book: {book.Title}, {mostBorrowed.Count()} times borrowed"
                : "Error");
        }

        private static void BorrowerAction(IBorrowerService borrowerService)
        {
            while (true)
            {
                Console.WriteLine(" \nBorrower actions-Menu");
                Console.WriteLine(" 1 - Butun Borrowerlarin siyahisi");
                Console.WriteLine(" 2 - Borrower yaratmaq");
                Console.WriteLine(" 3 - Borrower editlemek");
                Console.WriteLine(" 4 - Borrower silmek");
                Console.WriteLine(" 0-Exit");

                Console.WriteLine("Make your choice");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        foreach (var item in borrowerService.GetAll())
                        {
                            Console.WriteLine($"Id: {item.Id},Name: {item.Name},Email: {item.Email}");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter new borrower: Name,Email");
                        borrowerService.Create(new Borrower
                        {
                            Name = Console.ReadLine(),
                            Email = Console.ReadLine(),
                            IsDeleted = false,
                            CreatedAt = DateTime.UtcNow.AddHours(4),
                            UpdatedAt = DateTime.UtcNow.AddHours(4)
                        });
                        break;
                    case "3":
                        Console.WriteLine("Update borrower,enter borrower ID:");
                        int borrowerId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new borrower: Name,Email");
                        borrowerService.Update(borrowerId, new Borrower
                        {
                            Name = Console.ReadLine(),
                            Email = Console.ReadLine()
                        });
                        break;
                    case "4":
                        Console.WriteLine("The ID you want to delete");
                        int delete = int.Parse(Console.ReadLine());
                        borrowerService.Delete(delete);
                        break;
                    case "0":
                        Console.WriteLine("Exit program");
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }

            }
        }

        static void BookAction(IBookService bookService)
        {
            while (true)
            {
                Console.WriteLine(" \nBook actions-Menu");
                Console.WriteLine("  1 - Butun booklarin siyahisi");
                Console.WriteLine("  2 - Book yaratmaq");
                Console.WriteLine("  3 - Book editlemek");
                Console.WriteLine("  4 - Book silmek");
                Console.WriteLine("  0-Exit");

                Console.WriteLine("Make your choice");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        foreach (var item in bookService.GetAll())
                        {
                            Console.WriteLine($"Id:{item.Id},Title:{item.Title},Description:{item.Description},Pusblishedyear:{item.PublishedYear}");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter new book: Title,Description,Publishedyear");
                        bookService.Create(new Book
                        {
                            Title = Console.ReadLine(),
                            Description = Console.ReadLine(),
                            PublishedYear = int.Parse(Console.ReadLine()),
                            IsDeleted = false,
                            CreatedAt = DateTime.UtcNow.AddHours(4),
                            UpdatedAt = DateTime.UtcNow.AddHours(4)
                        });
                        break;
                    case "3":
                        Console.WriteLine("Update book,Enter book Id:");
                        int bookId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new book: Title,Description,Publishedyear");
                        bookService.Update(bookId, new Book
                        {
                            Title = Console.ReadLine(),
                            Description = Console.ReadLine(),
                            PublishedYear = int.Parse(Console.ReadLine()),
                        });
                        break;
                    case "4":
                        Console.WriteLine("The ID you want to delete");
                        int delete = int.Parse(Console.ReadLine());
                        bookService.Delete(delete);
                        Console.WriteLine($"{delete} is delete");
                        break;
                    case "0":
                        Console.WriteLine("Exit program");
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;

                }
            }
        }

          static void AuthorAction(IAuthorService authorServices)
        {
            while (true)
            {
                Console.WriteLine(" \nAuthor actions-Menu");
                Console.WriteLine(" 1 - Butun authorlarin siyahisi");
                Console.WriteLine(" 2 - Author yaratmaq");
                Console.WriteLine(" 3 - Author editlemek");
                Console.WriteLine(" 4 - Author silmek");
                Console.WriteLine(" 0-Exit");

                Console.WriteLine(" Make your choice");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        foreach (var item in authorServices.GetAll())
                        {
                            Console.WriteLine($"ID:{item.Id},Name: {item.Name}");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter Name");
                        authorServices.Create(new Author
                        {
                            Name = Console.ReadLine(),
                            IsDeleted = false,
                            UpdatedAt = DateTime.UtcNow.AddHours(4),
                            CreatedAt = DateTime.UtcNow.AddHours(4)
                        });
                        break;
                    case "3":
                        Console.WriteLine("Update author,Enter author ID:");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("New Author name:");
                        authorServices.Update(id, new Author { Name = Console.ReadLine(), IsDeleted = false, UpdatedAt = DateTime.UtcNow.AddHours(4), });
                        break;
                    case "4":
                        Console.WriteLine("The ID you want to delete");
                        int delete = int.Parse(Console.ReadLine());
                        authorServices.Delete(delete);
                        Console.WriteLine($"{delete} is delete");
                        break;
                    case "0":
                        Console.WriteLine("Exit program");
                        return;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;

                }

            }
        



          }
    }

}
    
