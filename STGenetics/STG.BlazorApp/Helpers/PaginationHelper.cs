namespace STG.BlazorApp.Helpers
{
    public class PaginationHelper<T>
    {
        private List<T> items;
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public int TotalPages => (int)Math.Ceiling((decimal)items.Count / PageSize);
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage == TotalPages;
        public bool IsPreviousPage => CurrentPage > 1;
        public bool IsNextPage => CurrentPage < TotalPages;

        public List<T> GetPageItems()
        {
            return items
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public void SetItems(List<T> items)
        {
            this.items = items;
        }

        public void GoToFirstPage()
        {
            CurrentPage = 1;
        }

        public void GoToPreviousPage()
        {
            CurrentPage--;
        }

        public void GoToNextPage()
        {
            CurrentPage++;
        }

        public void GoToLastPage()
        {
            CurrentPage = TotalPages;
        }
    }
}
