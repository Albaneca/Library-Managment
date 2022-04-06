using Library.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class UpdatePasswordViewModel
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = GlobalConstants.PASSWORDS_MUST_MATCH)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string ConfirmPassword { get; set; }
    }
}
