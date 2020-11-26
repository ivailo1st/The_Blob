using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using The_Blob.Models;

namespace The_Blob.Data
{
    public class BlobContext : DbContext
    {
        public BlobContext (DbContextOptions<BlobContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<CharacterFridge> CharacterFridge { get; set; }
        public DbSet<Fridge> Fridge { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
