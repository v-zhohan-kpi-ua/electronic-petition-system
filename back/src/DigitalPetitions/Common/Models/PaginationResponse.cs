namespace DigitalPetitions.Common.Models
{
    public class PaginationResponse<T>
    {
        public List<T> Items { get; set; }

        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public PaginationResponse(List<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
