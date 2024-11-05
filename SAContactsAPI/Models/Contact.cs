using System.ComponentModel.DataAnnotations;

namespace SAContactsAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"\S+", ErrorMessage = "First name cannot be empty or whitespace.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"\S+", ErrorMessage = "Last name cannot be empty or whitespace.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "A valid email is required.")]
        public string Email { get; set; } = string.Empty;
    }
}
