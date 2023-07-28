namespace GeekWorld.WebAPI.Commons
{
    public class Pageable
    {
        private static readonly int PAGE_DEFAULT = 1;
        private static readonly int LIMIT_DEFAULT = 10;
        private static readonly string SEARCH_DEFAULT = string.Empty;
        public int Page { get; set; } = PAGE_DEFAULT;
        public int Limit { get; set; } = LIMIT_DEFAULT;
        public string Search { get; set; } = SEARCH_DEFAULT;
    }
}
