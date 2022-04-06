using Library.Common;
using Library.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class SettingsViewModel
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

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string NewPassword { get; set; }

        public string Role { get; set; }

        public IFormFile ProfilePicture { get; set; }

        public string ProfilePictureLink { get; set; }

        public DateTime RegisteredAt { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
