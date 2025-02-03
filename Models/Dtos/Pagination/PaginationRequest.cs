namespace TagerProject.Models.Dtos.Pagination
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1; // Default to the first page
        public int PageSize { get; set; } = 10;  // Default to 10 items per page
    }
}
