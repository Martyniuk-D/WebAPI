using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;
using WebAPI_18.Data.ViewModels;

namespace WebAPI_18.Data.Services
{
    public class PublisherService
    {
        const int skipPages = 6;
        private readonly AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public PublisherPage GetAllPublishers(string sortBy, string searchString, int? page)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
            }
            if (page == null || page < 1)
            {
                page = 1;
            }
            PublisherPage publisherPage = new PublisherPage()
            {
                Count = allPublishers.Count,
                Prev = (int)page - 1 < 1 ? null : (int)page - 1,
                Next = (int)page + 1 > allPublishers.Count / skipPages ? null : (int)page + 1
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString)).ToList();
            }
            allPublishers = allPublishers.Skip(skipPages * ((int)page - 1)).Take(skipPages).ToList();

            publisherPage.Publishers = new List<PublisherVM>();
            foreach (var item in allPublishers)
            {
                publisherPage.Publishers.Add(new PublisherVM() { Name = item.Name });
            }

            return publisherPage;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            return _publisher;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();
            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Publisher with id: {id} not found.");
            }
        }
        public void UpdatePublisher(int id, PublisherVM publisher)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _publisher.Name = publisher.Name;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Publisher with id: {id} not found.");
            }
        }
    }
}
