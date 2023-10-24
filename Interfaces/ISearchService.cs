using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crito.Interfaces;

public interface ISearchService
{
    IEnumerable<IPublishedContent> SearchContentNames(string query);
}
