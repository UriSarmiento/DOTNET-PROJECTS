using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace COMMERCE_WEB_APP.Models
{
	public class Users
	{
		[Key]
		public int UserId { get; set; }
		[Required]
		[MinLength(6,ErrorMessage ="Username has to be at least 6 characters long")]
		[MaxLength(15)]
		public string UserName { get; set; }
		[Required]
		[MinLength(6, ErrorMessage = "Password has to be at least 6 characters long")]
		[MaxLength(15)]
		public string Password { get; set; }
	}
}
