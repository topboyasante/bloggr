using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bloggr.API.Models.Domain
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "First Name must be a maximum of 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "Last Name must be a maximum of 100 characters")]
        public string LastName { get; set; } = string.Empty;
    }
}