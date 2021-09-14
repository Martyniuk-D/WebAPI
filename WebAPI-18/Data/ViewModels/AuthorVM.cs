using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_18.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }
    public class AuthorPage
    {
        public int Count { get; set; }
        public int? Prev { get; set; }
        public int? Next { get; set; }
        public List<AuthorVM> Authors { get; set; }
    }
}
