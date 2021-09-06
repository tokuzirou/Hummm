using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Hummm.Models;

namespace Hummm.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Poster> Posters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //IdentityDbContextのデータ構造の定義
            base.OnModelCreating(builder);
        }
    }
}
