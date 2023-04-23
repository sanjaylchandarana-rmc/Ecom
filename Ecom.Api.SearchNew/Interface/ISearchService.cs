namespace Ecom.Api.Searches.Interfaces
{
    public interface ISearchService
    {
        Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId);
    }
}
