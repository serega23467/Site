using Microsoft.EntityFrameworkCore;
using Site.Models;
namespace Site.wwwroot.db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<NewsItemModel> News { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=SiteDB;Username=postgres;Password=sa");
        }
        public static string GetCategoryName(int id)
        {
            using (var context = new ApplicationContext())
            {
                var cat = context.Categories.FirstOrDefault(c => c.Id == id);
                if (cat != null)
                {
                    return cat.Title;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
