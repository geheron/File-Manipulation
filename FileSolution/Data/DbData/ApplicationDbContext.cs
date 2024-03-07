using Microsoft.EntityFrameworkCore;
using Models.FileModel;
using System;

namespace Data.DbData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FileClass> files { get; set; }
    }
}
