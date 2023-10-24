using Crito.Contexts;
using Crito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using Crito.Services;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Crito.Controllers;

public class SubscriptionController : SurfaceController
{
    protected readonly ContactContext _context;
    protected readonly SubscriptionService _subscriptionService;
    public SubscriptionController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, ContactContext context, SubscriptionService subscriptionService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _context = context;
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Index(SubscriptionForm subscriptionForm)
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();

        else
        {
            var email = await _subscriptionService.GetAsync(subscriptionForm);
            if (email == null)
            {
               await _subscriptionService.CreateAsync(subscriptionForm);
                return CurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }


    }
}
