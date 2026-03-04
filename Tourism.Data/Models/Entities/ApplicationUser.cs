using Microsoft.AspNetCore.Identity;

namespace Tourism.Data.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string ProfilePictureUrl { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
