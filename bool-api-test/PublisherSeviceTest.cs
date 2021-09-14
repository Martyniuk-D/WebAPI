using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAPI_18.Data;
using WebAPI_18.Data.Models;
using WebAPI_18.Data.Services;

namespace bool_api_test
{
    public class PublisherSeviceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Library").Options;

        AppDbContext _context;
        PublisherService _publisherService;
        AuthorService _authorService;
        BookService _bookService;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedData();
            _publisherService = new PublisherService(_context);
            _authorService = new AuthorService(_context);
            _bookService = new BookService(_context);

        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _publisherService.GetAllPublishers("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(2)]
        public void GetAllPublishers_WithSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _publisherService.GetAllPublishers("desc", "", null);
            string word = "Publisher 9";
            Assert.AreEqual(result.Publishers[0].Name, word, $"{result.Publishers[0].Name} != {word}");
        }
        [Test, Order(3)]
        public void GetAllPublishers_WithNoSort_WithSearch_WithNoPageNumber()
        {
            var result = _publisherService.GetAllPublishers("", "1", null);
            Assert.AreEqual(result.Publishers.Count, 2, $"{result.Publishers.Count} != {2}");
        }
        [Test, Order(4)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithPageNumber()
        {
            var result = _publisherService.GetAllPublishers("", "", 2);
            string word = "Publisher 6";
            Assert.AreEqual(result.Publishers[0].Name, word, $"{result.Publishers[0].Name} != {word}");
        }
        [Test, Order(5)]
        public void GetAllPublishers_WithSort_WithSearch_WithNoPageNumber()
        {
            var result = _publisherService.GetAllPublishers("desc", "1", null);
            string word = "Publisher 10";
            Assert.AreEqual(result.Publishers[0].Name, word, $"{result.Publishers[0].Name} != {word}");
        }
        [Test, Order(6)]
        public void GetAllPublishers_WithSort_WithSearch_WithPageNumber()
        {
            var result = _publisherService.GetAllPublishers("desc", "1", 2);
            Assert.AreEqual(result.Publishers.Count, 0, $"{result.Publishers.Count} != {0}");
        }

        [Test, Order(7)]
        public void GetPublishersById()
        {
            var result = _publisherService.GetPublisherById(1);
            string word = "Publisher 1";
            Assert.AreEqual(result.Name, word, $"{result.Name} != {word}");
        }
        [Test, Order(8)]
        public void AddPublisher()
        {
            string word = "Publisher 99";
            _publisherService.AddPublisher(new WebAPI_18.Data.ViewModels.PublisherVM() { Name = word });
            var result = _publisherService.GetPublisherById(11);
            Assert.AreEqual(result.Name, word, $"{result.Name} != {word}");
        }
        [Test, Order(9)]
        public void DeletePublisher()
        {
            _publisherService.DeletePublisherById(11);
            var result = _publisherService.GetAllPublishers("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(10)]
        public void UpdatePublisher()
        {
            string word = "Test";
            _publisherService.UpdatePublisher(9, new WebAPI_18.Data.ViewModels.PublisherVM() { Name = word });
            var result = _publisherService.GetPublisherById(9);
            Assert.AreEqual(result.Name, word, $"{result.Name} != {word}");
        }
        [Test, Order(11)]
        public void GetAllAuthors_WithNoSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _authorService.GetAllAuthors("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(12)]
        public void GetAllAuthors_WithSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _authorService.GetAllAuthors("desc", "", null);
            string word = "Author 9";
            Assert.AreEqual(result.Authors[0].FullName, word, $"{result.Authors[0].FullName} != {word}");
        }
        [Test, Order(13)]
        public void GetAllAuthors_WithNoSort_WithSearch_WithNoPageNumber()
        {
            var result = _authorService.GetAllAuthors("", "1", null);
            Assert.AreEqual(result.Authors.Count, 2, $"{result.Authors.Count} != {2}");
        }
        [Test, Order(14)]
        public void GetAllAuthors_WithNoSort_WithNoSearch_WithPageNumber()
        {
            var result = _authorService.GetAllAuthors("", "", 2);
            string word = "Author 6";
            Assert.AreEqual(result.Authors[0].FullName, word, $"{result.Authors[0].FullName} != {word}");
        }
        [Test, Order(15)]
        public void GetAllAuthors_WithSort_WithSearch_WithNoPageNumber()
        {
            var result = _authorService.GetAllAuthors("desc", "1", null);
            string word = "Author 10";
            Assert.AreEqual(result.Authors[0].FullName, word, $"{result.Authors[0].FullName} != {word}");
        }
        [Test, Order(16)]
        public void GetAllAuthors_WithSort_WithSearch_WithPageNumber()
        {
            var result = _authorService.GetAllAuthors("desc", "1", 2);
            Assert.AreEqual(result.Authors.Count, 0, $"{result.Authors.Count} != {0}");
        }
        [Test, Order(17)]
        public void GetAuthorsById()
        {
            var result = _authorService.GetAuthorsById(1);
            string word = "Author 1";
            Assert.AreEqual(result.FullName, word, $"{result.FullName} != {word}");
        }
        [Test, Order(18)]
        public void AddAuthor()
        {
            string word = "Author 99";
            _authorService.AddAuthor(new WebAPI_18.Data.ViewModels.AuthorVM() { FullName = word });
            var result = _authorService.GetAuthorsById(11);
            Assert.AreEqual(result.FullName, word, $"{result.FullName} != {word}");
        }
        [Test, Order(19)]
        public void DeleteAuthor()
        {
            _authorService.DeleteAuthorsById(11);
            var result = _authorService.GetAllAuthors("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(20)]
        public void UpdateAuthor()
        {
            string word = "Test";
            _authorService.UpdateAuthors(9, new WebAPI_18.Data.ViewModels.AuthorVM() { FullName = word });
            var result = _authorService.GetAuthorsById(9);
            Assert.AreEqual(result.FullName, word, $"{result.FullName} != {word}");
        }
        [Test, Order(21)]
        public void GetAllBooks_WithNoSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _bookService.GetAllBooks("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(22)]
        public void GetAllBooks_WithSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _bookService.GetAllBooks("desc", "", null);
            string word = "Book 9";
            Assert.AreEqual(result.Books[0].Title, word, $"{result.Books[0].Title} != {word}");
        }
        [Test, Order(23)]
        public void GetAllBooks_WithNoSort_WithSearch_WithNoPageNumber()
        {
            var result = _bookService.GetAllBooks("", "1", null);
            Assert.AreEqual(result.Books.Count, 2, $"{result.Books.Count} != {2}");
        }
        [Test, Order(24)]
        public void GetAllBooks_WithNoSort_WithNoSearch_WithPageNumber()
        {
            var result = _bookService.GetAllBooks("", "", 2);
            string word = "Book 6";
            Assert.AreEqual(result.Books[0].Title, word, $"{result.Books[0].Title} != {word}");
        }
        [Test, Order(25)]
        public void GetAllBooks_WithSort_WithSearch_WithNoPageNumber()
        {
            var result = _bookService.GetAllBooks("desc", "1", null);
            string word = "Book 10";
            Assert.AreEqual(result.Books[0].Title, word, $"{result.Books[0].Title} != {word}");
        }
        [Test, Order(26)]
        public void GetAllBooks_WithSort_WithSearch_WithPageNumber()
        {
            var result = _bookService.GetAllBooks("desc", "1", 2);
            Assert.AreEqual(result.Books.Count, 0, $"{result.Books.Count} != {0}");
        }
        [Test, Order(27)]
        public void GetBookById()
        {
            var result = _bookService.GetBookById(1);
            string word = "Book 1";
            Assert.AreEqual(result.Title, word, $"{result.Title} != {word}");
        }
        [Test, Order(28)]
        public void AddBook()
        {
            string word = "Book 99";
            _bookService.AddBookWithAuthors(new WebAPI_18.Data.ViewModels.BookVM() 
            { Title = word, Rate=1,PublisherId=2,IsRead=true,
                Genre="Some genre", ImageURL="Some URL", Description="Bla", DateRead=new DateTime(2000,1,1),
                DateAdded= new DateTime(2000, 1, 1), AuthorIds=null 
            });
            var result = _bookService.GetBookById(11);
            Assert.AreEqual(result.Title, word, $"{result.Title} != {word}");
        }
        [Test, Order(29)]
        public void DeleteBook()
        {
            _bookService.DeleteBook(11);
            var result = _bookService.GetAllBooks("", "", null);
            Assert.AreEqual(result.Count, 10, $"{result.Count} != {10}");
        }
        [Test, Order(30)]
        public void UpdateBook()
        {
            string word = "Test";
            _bookService.UpdateBook(9, new WebAPI_18.Data.ViewModels.BookVM() { Title = word });
            var result = _bookService.GetBookById(9);
            Assert.AreEqual(result.Title, word, $"{result.Title} != {word}");
        }

        private void SeedData()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                },
                new Publisher()
                {
                    Id = 7,
                    Name = "Publisher 7"
                },
                new Publisher()
                {
                    Id = 8,
                    Name = "Publisher 8"
                },
                new Publisher()
                {
                    Id = 9,
                    Name = "Publisher 9"
                },
                new Publisher()
                {
                    Id = 10,
                    Name = "Publisher 10"
                }
            };
            _context.Publishers.AddRange(publishers);

            var authors = new List<Author>
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                },
                new Author()
                {
                    Id = 3,
                    FullName = "Author 3"
                },
                new Author()
                {
                    Id = 4,
                    FullName = "Author 4"
                },
                new Author()
                {
                    Id = 5,
                    FullName = "Author 5"
                },
                new Author()
                {
                    Id = 6,
                    FullName = "Author 6"
                },
                new Author()
                {
                    Id = 7,
                    FullName = "Author 7"
                },
                new Author()
                {
                    Id = 8,
                    FullName = "Author 8"
                },
                new Author()
                {
                    Id = 9,
                    FullName = "Author 9"
                },
                new Author()
                {
                    Id = 10,
                    FullName = "Author 10",
                }
            };

            _context.Authors.AddRange(authors);

            var books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 3,
                    Title = "Book 3",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 4,
                    Title = "Book 4",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 5,
                    Title = "Book 5",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 6,
                    Title = "Book 6",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 7,
                    Title = "Book 7",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 8,
                    Title = "Book 8",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 9,
                    Title = "Book 9",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                },
                new Book()
                {
                    Id = 10,
                    Title = "Book 10",
                    DateAdded = new DateTime(2020,12,22),
                    DateRead = new DateTime(2021,10,11),
                    Description = "Bla-bla",
                    Genre = "Some genre",
                    ImageURL = "Some URL",
                    IsRead = true,
                    Rate = 5,
                    PublisherId=1
                }
            };

            _context.Books.AddRange(books);

            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}