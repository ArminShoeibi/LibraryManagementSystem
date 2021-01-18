using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs
{
    public record UpdatePasswordDto
    {

        public int ApplicationUserId { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; init; }
    }
}
