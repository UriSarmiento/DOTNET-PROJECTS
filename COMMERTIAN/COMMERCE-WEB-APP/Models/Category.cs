using System.ComponentModel.DataAnnotations;

namespace COMMERCE_WEB_APP.Models
{
    public class Category
    {
        [Key] //The explicit way to set a Primary key
        public int Id { get; set; } // Primary Key, the name is just Id or the model name + Id this parameter will be automatically recognized as the Primary Key
        [Required] //This sets the parameter for the table as Not Null
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
