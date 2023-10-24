using Crito.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Crito.Models;

public class ContactForm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Message { get; set; } = null!;
    public string? ReDirectUrl { get; set; } = "/";

    public static implicit operator ContactEntity(ContactForm contactForm)
    {
        return new ContactEntity
        {
            Description = contactForm.Message,
            Name = contactForm.Name,
            Email = contactForm.Email,
            ContactUmbracoKey = Guid.NewGuid(),
        };
    }
}
