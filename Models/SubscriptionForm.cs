using Crito.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Crito.Models;

public class SubscriptionForm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public static implicit operator SubscriptionEntity(SubscriptionForm subscriptionForm)
    {
        return new SubscriptionEntity 
        {
            Email = subscriptionForm.Email,
        };
    }
}
