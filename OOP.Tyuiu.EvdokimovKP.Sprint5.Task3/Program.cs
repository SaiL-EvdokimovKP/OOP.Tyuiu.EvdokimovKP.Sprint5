using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public delegate void BookHandler(string message);
    public delegate string BookInfoDelegate();

    // Вариант 5. Общие данные и действия книг.
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }

        // Три дополнительных свойства базового класса.
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int Rating { get; set; } = 5;
        public event BookHandler BookChanged;

        public Book(string title, string author, int year, decimal price)
        {
            Title = title;
            Author = author;
            YearOfPublication = year;
            Price = price;
        }

        public virtual string GetInfo()
        {
            return $"\"{Title}\" - {Author} ({YearOfPublication} г.) | Цена: {Price} руб.";
        }

        public virtual void Read()
        {
            Console.WriteLine("Вы читаете книгу: " + Title);
        }

        public virtual void Borrow()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine("Книга выдана: " + Title);
                if (BookChanged != null)
                {
                    BookChanged("Выдача книги: " + Title);
                }
            }
            else
            {
                Console.WriteLine("Книга недоступна: " + Title);
            }
        }

        // Три дополнительных метода базового класса.
        public void ApplyDiscount(decimal percent)
        {
            Price -= Price * percent / 100m;
            Console.WriteLine("Цена со скидкой: " + Price);
        }

        public void ReturnBook()
        {
            IsAvailable = true;
            Console.WriteLine("Книга возвращена: " + Title);
        }

        public void ShowRating()
        {
            Console.WriteLine("Оценка книги: " + Rating);
        }
    }

    public class Textbook : Book
    {
        public string Subject { get; set; }

        // Три дополнительных свойства учебника.
        public int Grade { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; } = "";

        public Textbook(string title, string author, int year, decimal price, string subject)
            : base(title, author, year, price)
        {
            Subject = subject;
        }

        public override void Read()
        {
            Console.WriteLine($"Вы изучаете учебник \"{Title}\" по предмету {Subject}.");
        }

        public void ShowGrade()
        {
            Console.WriteLine("Класс: " + Grade);
        }

        public void ShowPageCount()
        {
            Console.WriteLine("Количество страниц: " + PageCount);
        }

        public void ShowPublisher()
        {
            Console.WriteLine("Издательство: " + Publisher);
        }
    }

    public class Fiction : Book
    {
        public string Genre { get; set; }

        // Три дополнительных свойства художественной книги.
        public string Series { get; set; } = "";
        public int PageCount { get; set; }
        public string Language { get; set; } = "";

        public Fiction(string title, string author, int year, decimal price, string genre)
            : base(title, author, year, price)
        {
            Genre = genre;
        }

        public override void Borrow()
        {
            base.Borrow();
            Console.WriteLine("Жанр: " + Genre);
        }

        public void ShowSeries()
        {
            Console.WriteLine("Серия: " + Series);
        }

        public void ShowPageCount()
        {
            Console.WriteLine("Количество страниц: " + PageCount);
        }

        public void ShowLanguage()
        {
            Console.WriteLine("Язык: " + Language);
        }
    }

    public class ScientificLiterature : Book
    {
        public string FieldOfScience { get; set; }

        // Три дополнительных свойства научной книги.
        public string ResearchTopic { get; set; } = "";
        public int PageCount { get; set; }
        public string Institute { get; set; } = "";

        public ScientificLiterature(string title, string author, int year, decimal price, string field)
            : base(title, author, year, price)
        {
            FieldOfScience = field;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + " | Область науки: " + FieldOfScience;
        }

        public void ShowTopic()
        {
            Console.WriteLine("Тема исследования: " + ResearchTopic);
        }

        public void ShowPageCount()
        {
            Console.WriteLine("Количество страниц: " + PageCount);
        }

        public void ShowInstitute()
        {
            Console.WriteLine("Институт: " + Institute);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Textbook textbook = new Textbook("Математика", "Иванов И.И.", 2021, 1200m, "Алгебра");
            textbook.Grade = 8;
            textbook.PageCount = 200;
            textbook.Publisher = "Просвещение";

            Fiction novel = new Fiction("Преступление и наказание", "Достоевский Ф.М.", 1866, 600m, "Роман");
            novel.Series = "Классика";
            novel.PageCount = 600;
            novel.Language = "Русский";

            ScientificLiterature physics = new ScientificLiterature("Квантовая физика", "Петров П.П.", 2019, 1500m, "Физика");
            physics.ResearchTopic = "Кванты";
            physics.PageCount = 300;
            physics.Institute = "ТюмГУ";

            // Коллекция хранит книги разных типов. Делегат вызывает их GetInfo().
            List<Book> books = new List<Book> { textbook, novel, physics };
            foreach (Book book in books)
            {
                book.BookChanged += OnBookChanged;
                BookInfoDelegate displayBook = book.GetInfo;
                Console.WriteLine(displayBook());
                book.Read();
                book.Borrow();
                book.ShowRating();
            }

            Console.WriteLine("Дополнительные сведения:");
            textbook.ShowGrade();
            textbook.ShowPageCount();
            textbook.ShowPublisher();
            novel.ShowSeries();
            novel.ShowPageCount();
            novel.ShowLanguage();
            physics.ShowTopic();
            physics.ShowPageCount();
            physics.ShowInstitute();

            // Повторная выдача недоступной книги и возврат.
            novel.Borrow();
            novel.ReturnBook();
            novel.Borrow();
            textbook.ApplyDiscount(10m);
            Console.WriteLine(textbook.GetInfo());
        }

        static void OnBookChanged(string message)
        {
            Console.WriteLine("Событие: " + message);
        }
    }
}