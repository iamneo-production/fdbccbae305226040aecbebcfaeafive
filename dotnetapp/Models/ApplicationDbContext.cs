using dotnetapp.Models;
using System;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



public class ApplicationDbContext : DbContext
    {
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        // Write your ApplicationDbContext here...
    }