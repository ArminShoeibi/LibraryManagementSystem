namespace LibraryManagementSystem.DTOs
{
    public record ApplicationUserDetailDto 
    {
        public int ApplicationUserId { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
    }
 
}
