using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs
{
    public record CreateApplicationUserDto
    {

        [Required]
        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; init; }


        [Required]
        [StringLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; init; }

        [Required]
        [StringLength(40)]
        public string UserName { get; init; }

        [EmailAddress]
        [Required]
        public string Email { get; init; }


        [Required]
        [StringLength(11,MinimumLength = 8)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; init; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; init; }
    }
}
