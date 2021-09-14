using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;
using WebAPI_18.Data.ViewModels;

namespace WebAPI_18.Data.Services
{
    public class AuthorService
    {
        const int skipPages = 6;
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
            return _author;
        }

        public AuthorPage GetAllAuthors(string sortBy, string searchString, int? page)
        {
            var allAuthors = _context.Authors.OrderBy(n => n.FullName).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                allAuthors = allAuthors.OrderByDescending(n => n.FullName).ToList();
            }
            if (page == null || page < 1)
            {
                page = 1;
            }
            AuthorPage authorPage = new AuthorPage()
            {
                Count = allAuthors.Count,
                Prev = (int)page - 1 < 1 ? null : (int)page - 1,
                Next = (int)page + 1 > allAuthors.Count / skipPages ? null : (int)page + 1
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                allAuthors = allAuthors.Where(n => n.FullName.Contains(searchString)).ToList();
            }
            allAuthors = allAuthors.Skip(skipPages * ((int)page - 1)).Take(skipPages).ToList();

            authorPage.Authors = new List<AuthorVM>();
            foreach (var item in allAuthors)
            {
                authorPage.Authors.Add(new AuthorVM() { FullName = item.FullName });
            }

            return authorPage;
        }

        public Author GetAuthorsById(int id) => _context.Authors.FirstOrDefault(n => n.Id == id);

        public void DeleteAuthorsById(int id)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);
            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Author with id: {id} not found.");
            }
        }

        public void UpdateAuthors(int id, AuthorVM author)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);
            if (_author != null)
            {
                _author.FullName = author.FullName;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Publisher with id: {id} not found.");
            }
        }
    }
}
