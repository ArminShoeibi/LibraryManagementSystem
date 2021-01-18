using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs
{
    public record LoginDto
    {
        [Required]
        [StringLength(40)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}
