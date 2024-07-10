namespace SharpTables.Pagination
{

    /// <summary>
    /// Represents a pagination interface.
    /// </summary>
    /// <typeparam name="T">The type of elements in the pagination.</typeparam>
    public interface IPagination<T> : IEnumerable<T> where T : class
    {
        /// <summary>
        /// Gets the current page index.
        /// </summary>
        public int CurrentPageIndex { get; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the current element.
        /// </summary>
        public T Current { get; }

        /// <summary>
        /// Gets the list of pages.
        /// </summary>
        public List<T> Pages { get; }

        /// <summary>
        /// Prints the specified page.
        /// </summary>
        /// <param name="page">The page number to print.</param>
        public void PrintPage(int page);

        /// <summary>
        /// Prints the next page.
        /// </summary>
        public void PrintNext();

        /// <summary>
        /// Prints the current page.
        /// </summary>
        public void PrintCurrent();

        /// <summary>
        /// Prints the previous page.
        /// </summary>
        public void PrintPrevious();

        /// <summary>
        /// Goes to the specified page.
        /// </summary>
        /// <param name="page">The page number to go to.</param>
        public void GoToPage(int page);
    }
}
