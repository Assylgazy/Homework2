using System.Drawing.Printing;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Reddit.Models;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class CommunitiesRepository : ICommunitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunitiesRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<PagedList<CommunitiesRepository>> GetCommunities(int pageNumber, int pageSize, bool? isAscending = true, string? sortKey = null)
        {
            var communities = _context.Communities.AsQueryable();

            if (isAscending == false)
            {
                communities = communities.OrderByDescending(GetSortExpression(sortKey));
            }
            else
            {
                communities = communities.OrderBy(GetSortExpression(sortKey));
            }




            return await PagedList<CommunitiesRepository>.CreateAsync(communities, pageNumber, pageSize);
        }
        public Expression<Func<Community, object>> GetSortExpression(string? sortKey)
        {
            sortKey = sortKey?.ToLower();
            return sortKey switch
            {
                "positivity" => community => (community.Upvotes) / (community.Upvotes + community.Downvotes),
                "popular" => community => community.Upvotes + community.Downvotes,
                _ => community => community.Id
            };
        }
    }
}





