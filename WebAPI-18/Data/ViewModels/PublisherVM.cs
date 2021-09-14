using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;

namespace WebAPI_18.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; } 
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }

    public class PublisherPage
    {
        public int Count { get; set; }
        public int? Prev { get; set; }
        public int? Next { get; set; }
        public List<PublisherVM> Publishers{ get; set; }
    }
}
