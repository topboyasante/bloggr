using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bloggr.API.Models.DTO.Auth
{
    public class SignUpRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "This must be a valid email")]
        public required string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Username must be a minimum of 2 characters")]
        [MaxLength(20, ErrorMessage = "Username must be a maximum of 100 characters")]
        [JsonPropertyName("user_name")]
        public required string UserName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "First Name must be a maximum of 100 characters")]
        [JsonPropertyName("first_name")]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "Last Name must be a maximum of 100 characters")]
        [JsonPropertyName("last_name")]
        public required string LastName { get; set; }

        [Required]
        [Phone]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(2, ErrorMessage = "Password must be a minimum of 2 characters")]
        public required string Password { get; set; }

        [Required]
        public required string[] Roles { get; set; }
    }
}