﻿using Library.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
    }
}
