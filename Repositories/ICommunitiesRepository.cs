using Reddit.Models;


namespace Reddit.Repositories
{
    public interface ICommunitiesRepository
    {
        public Task<PagedList<CommunitiesRepository>> GetCommunities(int pageNumber, int pageSize, bool? isAscending = true, string? sortKey = null);
    }
}
