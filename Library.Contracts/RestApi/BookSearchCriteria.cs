using Library.Contracts.Common;

namespace Library.Contracts.RestApi
{
    public class BookSearchCriteria
    {
        public SearchBy By { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
    }
}