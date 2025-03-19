using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
