using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedData();
            _publisherService = new PublisherService(_context);

        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSort_WithNoSearch_WithNoPageNumber()
        {
            var result = _publisherService.GetAllPublishers("", "", null);
            Assert.That(result.Count, Is.EqualTo(6));
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
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
    }
}