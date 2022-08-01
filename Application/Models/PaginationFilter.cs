namespace Application.Models
{
    public class PaginatedFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; } = int.MaxValue;

        public string OrderBy { get; set; } = null;

        public bool OrderIsDescending { get; set; } = false;

        public SearchFilter SearchFilter { get; set; }
    }
}
