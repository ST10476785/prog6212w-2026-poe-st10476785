using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RaceDayMVC.Models
{
    public class CategoryViewModelcs // CategoryViewModel class represents the data model for a category in the RaceDayMVC for different event categories
    {
        public int CategoryId { get; set; }

        [Required]

        [DisplayName("Category Name")]
        public string CategoryName { get; set; } = string.Empty;


        [DisplayName("Description")]
        public string? CategoryDescription { get; set; }

        [Required]
        public int EventId { get; set; }

    }
}
