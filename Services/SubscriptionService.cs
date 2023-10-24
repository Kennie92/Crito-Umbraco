using Crito.Contexts;
using Crito.Models;
using Crito.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crito.Services;

public class SubscriptionService
{
    protected readonly ContactContext _context;

    public SubscriptionService(ContactContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(SubscriptionForm subscriptionForm)
    {
        var EmailExist = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Email == subscriptionForm.Email);

        //If Email does not exist, add it into database
        if(EmailExist == null)
        {
            await _context.Subscriptions.AddAsync(subscriptionForm);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<SubscriptionEntity> GetAsync(SubscriptionForm subscriptionForm)
    {
        var EmailExist = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Email == subscriptionForm.Email);

        return EmailExist!;
    }
}
