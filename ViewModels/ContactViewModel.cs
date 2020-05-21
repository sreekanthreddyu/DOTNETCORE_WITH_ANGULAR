using System;
using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(200,ErrorMessage ="Max allowed length is 200 characters")]
        public string Message { get; set; }
    }
}