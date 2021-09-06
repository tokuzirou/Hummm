using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hummm.Models;

namespace Hummm.Data
{
    public class SeedData
    {
        public static void Initial(IServiceProvider serviceProvider)
        {
            using(var _context 
                = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                Poster poster = new Poster
                {
                    PosterID = 0,
                    PosterName = "Tokuma"
                };
                _context.Posters.Add(poster);

                Consumer consumer = new Consumer
                {
                    ConsumerID = 0,
                    SearchString = "a",
                    SearchDate = DateTime.Now
                };
                _context.Consumers.Add(consumer);
                _context.SaveChanges();
            }
        }
    }
}
