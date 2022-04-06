using Library.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class User : DeletableEntity
    {
        [MinLength(2)]
        [MaxLength(20)]
        public string Username { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(
            GlobalConstants.PassRegex,
            ErrorMessage = GlobalConstants.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public long? ApplicationRoleId { get; set; } = 4;

        public virtual string ProfilePictureURL { get; set; } = GlobalConstants.DefaultPicture;

        public virtual Ban Ban { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }

        public virtual ICollection<Inbox> InboxMessages { get; set; } 

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
