﻿using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp__Library_Management_Application_.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }


        public DbSet<BookAuthor> BookAuthor { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<LoanItem> LoanItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LCPUDH4\\SQLEXPRESS;Database=PB503MiniProject;Trusted_Connection=True;TrustServerCertificate=True;");
              base.OnConfiguring(optionsBuilder);
        }
    }
}
