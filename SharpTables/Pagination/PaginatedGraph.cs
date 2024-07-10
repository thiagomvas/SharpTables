using SharpTables.Graph;
using System.Collections;

namespace SharpTables.Pagination
{
    public class PaginatedGraph<T> : IPagination<Graph<T>>
    {
        public List<T> Values = new();
        public GraphSettings<T> Settings = new();
        public GraphFormatting Formatting = new();
        public int ColumnsPerPage { get; set; } = 7;

        public int CurrentPageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public Graph<T> Current { get; private set; }

        public List<Graph<T>> Pages { get; set; }
        /// <inheritdoc/>
        public void PrintPage(int page)
        {
            if (page < 0 || page > TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            Pages[page].Write();
        }

        /// <inheritdoc/>
        public void PrintCurrent()
        {
            PrintPage(CurrentPageIndex);
        }

        /// <inheritdoc/>
        public void PrintNext()
        {
            if (CurrentPageIndex < TotalPages - 1)
            {
                CurrentPageIndex++;
                PrintCurrent();
            }
        }

        /// <inheritdoc/>
        public void PrintPrevious()
        {
            if (CurrentPageIndex >= 1)
            {
                CurrentPageIndex--;
                PrintCurrent();
            }
        }

        /// <inheritdoc/>
        public void GoToPage(int page)
        {
            if (page < 0 || page > TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            CurrentPageIndex = page;
        }

        /// <inheritdoc/>
        public IEnumerator<Graph<T>> GetEnumerator()
        {
            return Pages.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
