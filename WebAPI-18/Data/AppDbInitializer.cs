using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_18.Data.Models;

namespace WebAPI_18.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicatinBuilder)
        {
            using (var serviceScope = applicatinBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "The Noise: A Thriller",
                            Description = "In the shadow of Mount Hood, sixteen-year-old Tennant is checking rabbit traps with her eight-year-old sister Sophie when the girls are suddenly overcome by a strange vibration rising out of the forest, building in intensity until it sounds like a deafening crescendo of screams. From out of nowhere, their father sweeps them up and drops them through a trapdoor into a storm cellar. But the sound only gets worse . . .",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-35),
                            Rate = 4,
                            Genre = "Thriller",
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/I/41TM-a1v60L._SX321_BO1,204,203,200_.jpg",
                            DateAdded = DateTime.Now.AddDays(-65)
                        },
                        new Book()
                        {
                            Title = "The Old Farmer's Almanac 2022",
                            Description = "Happy New Almanac Year! It’s time to celebrate the 230th edition of The Old Farmer’s Almanac! Long recognized as North America’s most-beloved and best-selling annual, this handy yellow book fulfills every need and expectation as a calendar of the heavens, a time capsule of the year, an essential reference that reads like a magazine. Always timely, topical, and distinctively “useful, with a pleasant degree of humor,” the Almanac is consulted daily throughout the year by users from all walks of life.",
                            IsRead = false,
                            Genre = "Detective",
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/I/51tpY0oLw3L._SX330_BO1,204,203,200_.jpg",
                            DateAdded = DateTime.Now.AddDays(-25)
                        }
                        
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
