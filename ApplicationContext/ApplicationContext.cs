
using classwork.Models;
using Microsoft.EntityFrameworkCore;

namespace classwork.ApplicationDbContext
{
    public class ApplicationContext : DbContext
    {
     
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options)
        {
            
        }
        public DbSet<FileOnDatabaseModel> FileOnDatabaseModels  {get; set;}
        public DbSet<FileOnFileSystemModel> FileOnFileSystemModels  {get; set;}
    
    }
}