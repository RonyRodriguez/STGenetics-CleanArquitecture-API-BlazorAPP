namespace STG.BlazorApp.Helpers
{
    public class FilteringHelper<T>
    {
        private List<T> items;

        public FilteringHelper(List<T> items)
        {
            this.items = items;
        }

        public List<T> ApplyFilters(Func<T, bool> filter)
        {
            return items.Where(filter).ToList();
        }
    }
}
