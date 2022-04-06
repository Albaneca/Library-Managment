using Library.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = GlobalConstants.VALUE_LENGTH_ERROR)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(GlobalConstants.PhoneRegex, ErrorMessage = GlobalConstants.WRONG_PHONE)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
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
