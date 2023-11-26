using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace COMMERCE_WEB_APP.Models
{
    public class Category
    {
        [Key] //The explicit way to set a Primary key
        public int CategoryId { get; set; } // Primary Key, the name is just Id or the model name + Id this parameter will be automatically recognized as the Primary Key
        [Required] //This sets the parameter for the table as Not Null
        [MinLength(3,ErrorMessage ="Category name must have at least 3 characters")]
        [MaxLength(20)]
        [DisplayName ("Category Name")] //Here we set the name a parameter will be diplayed with in the view
        public string Name { get; set; }
        [DisplayName ("Display Order")]
        [Range (1,100,ErrorMessage ="The display order must be between 1-100")] // We can set custom error messages like this
        public int DisplayOrder { get; set; }
    }
}
