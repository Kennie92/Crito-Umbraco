using Crito.Contexts;
using Crito.Models;
using Crito.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Crito.Controllers;

public class ContactsController : SurfaceController
{

    protected readonly ContactContext _contactContext;
    public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, ContactContext contactContext) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _contactContext = contactContext;
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactForm contactForm)
    {
        if(ModelState.IsValid)
        {
            //saving it to umbracos sqlite database
            await _contactContext.Contacts.AddAsync(contactForm);
            await _contactContext.SaveChangesAsync();

            using var mail = new MailService("no-reply@crito.com", "smtp.crito.com", 587, "contactform@crito.com", "lösenord");
            //To sender
            await mail.SendAsync(contactForm.Email, "Your request was recieved.", "Hi, your Request was recieved! and we will be in contact with you as soon as possible.").ConfigureAwait(false);

            //To Reciever
            await mail.SendAsync("Jan.Eriksson@crito.com", $"{contactForm.Name} sent a contact request", contactForm.Message).ConfigureAwait(false);

            TempData["Success"] = "Forms was submitted.";
        }
        else
        {
            TempData["ErrorMessage"] = "Form was not submitted.";
        }
        return LocalRedirect("/contacts");

    }
}
