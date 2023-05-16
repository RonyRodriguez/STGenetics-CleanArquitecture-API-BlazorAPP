namespace STG.BlazorApp.Helpers
{
    public class SortingHelper<T>
    {
        private List<T> items;

        public SortingHelper(List<T> items)
        {
            this.items = items;
        }

        public void SortByProperty<TKey>(Func<T, TKey> keySelector, bool ascending = true)
        {
            items = ascending
                ? items.OrderBy(keySelector).ToList()
                : items.OrderByDescending(keySelector).ToList();
        }

        public void SortByPropertyDescending<TKey>(Func<T, TKey> keySelector)
        {
            items = items.OrderByDescending(keySelector).ToList();
        }

        public List<T> GetSortedItems()
        {
            return items;
        }
    }
}
