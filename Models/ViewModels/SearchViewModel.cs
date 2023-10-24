using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crito.Models.ViewModels;

public class SearchViewModel : PublishedContentWrapped
{
    public SearchViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
    {
    }

    public IEnumerable<IPublishedContent> SearchResults { get; set; } = Enumerable.Empty<IPublishedContent>();
    public bool HasSearched { get; set; }
    public string SearchValue { get; set; } = null!;
}
