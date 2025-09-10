namespace DatadrivenApplicationEFCore.Utilities
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalNumofPages { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalNumofPages;

        public PagedList(List<T> items, int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            TotalNumofPages = (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }
    }
}
