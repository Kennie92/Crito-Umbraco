using Crito.Interfaces;
using Examine;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;

namespace Crito.Services;

public class SearchService : ISearchService
{
    private readonly IExamineManager _examineManager;
    private readonly UmbracoHelper _umbracoHelper;

    public SearchService(IExamineManager examineManager, UmbracoHelper umbracoHelper)
    {
        _examineManager = examineManager;
        _umbracoHelper = umbracoHelper;
    }

    public IEnumerable<IPublishedContent> SearchContentNames(string query)
    {
        IEnumerable<string> ids = Array.Empty<string>();
        if (!string.IsNullOrEmpty(query) && _examineManager.TryGetIndex("ExternalIndex", out IIndex? index))
        {
            ids = index
                .Searcher
                .CreateQuery("content")
                .NodeTypeAlias("articleitemPage")
                .And()
                .Field("nodeName", query)
                .Execute()
                .Select(x => x.Id);
        }

        foreach (var id in ids)
        {
            yield return _umbracoHelper.Content(id)!;
        }
    }
}
