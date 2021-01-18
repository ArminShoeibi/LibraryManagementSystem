using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationContext  : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public ApplicationContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
