using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;
using WebAPI_18.Data.ViewModels;

namespace WebAPI_18.Data.Services
{
    public class BookService
    {
        const int skipPages = 6;
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public BookPage GetAllBooks(string sortBy, string searchString, int? page)
        {
            var allBooks = _context.Books.OrderBy(n => n.Title).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                allBooks = allBooks.OrderByDescending(n => n.Title).ToList();
            }
            if (page == null || page < 1)
            {
                page = 1;
            }
            BookPage bookPage = new BookPage()
            {
                Count = allBooks.Count,
                Prev = (int)page - 1 < 1 ? null : (int)page - 1,
                Next = (int)page + 1 > allBooks.Count / skipPages ? null : (int)page + 1
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                allBooks = allBooks.Where(n => n.Title.Contains(searchString)).ToList();
            }
            allBooks = allBooks.Skip(skipPages * ((int)page - 1)).Take(skipPages).ToList();

            bookPage.Books = new List<BookVM>();
            foreach (var item in allBooks)
            {
                bookPage.Books.Add(new BookVM()
                {
                    Rate = item.Rate,
                    IsRead = item.IsRead,
                    ImageURL = item.ImageURL,
                    Genre = item.Genre,
                    Description = item.Description,
                    DateRead = item.DateRead,
                    DateAdded = item.DateAdded,
                    Title = item.Title,
                    PublisherId = item.PublisherId
                });
            }

            return bookPage;
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                ImageURL = book.ImageURL,
                DateAdded = book.DateAdded,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            if (book.AuthorIds != null)
            {
                foreach (var id in book.AuthorIds)
                {
                    var _book_author = new Book_Author()
                    {
                        BookId = _book.Id,
                        AuthorId = id
                    };
                    _context.Book_Authors.Add(_book_author);
                    _context.SaveChanges();
                }
            }
        }

        public BookWithAuthorVM GetBookById(int bookId)
        {
            var _book = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                ImageURL = book.ImageURL,
                DateAdded = book.DateAdded,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _book;
        }


        public void DeleteBook(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);

            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }

        public void UpdateBook(int id, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == id);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Rate = book.Rate;
                _book.IsRead = book.IsRead;
                _book.ImageURL = book.ImageURL;
                _book.Genre = book.Genre;
                _book.Description = book.Description;
                _book.DateRead = book.DateRead;
                _book.DateAdded = book.DateAdded;

                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Book with id: {id} not found.");
            }
        }
    }
}