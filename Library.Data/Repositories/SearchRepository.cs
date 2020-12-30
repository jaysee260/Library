namespace Library.Data.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly LibraryDbContext _dbContext;

        public SearchRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}