using System.Collections;

namespace SharpTables.Pagination
{
    /// <summary>
    /// Encapsulates a list of tables and provides methods to navigate through them. Implements <see cref="IPagination{T}"/> and <see cref="IEnumerable{T}"/>.
    /// </summary>
    public class PaginatedTable : IPagination<Table>
    {
        private int _currentPage = 0;
        /// <inheritdoc/>
        public int CurrentPageIndex
        {
            get => _currentPage;
            private set
            {
                if (value < 0 || value > TotalPages)
                {
                    return;
                }

                _currentPage = value;
            }
        }
        /// <inheritdoc/>
        public int TotalPages => Pages.Count;

        /// <inheritdoc/>
        public Table Current => Pages[CurrentPageIndex];

        /// <inheritdoc/>

        public List<Table> Pages { get; set; }

        /// <summary>
        /// Creates a new instance of PaginatedTable with the given pages.
        /// </summary>
        /// <param name="pages">The tables to be used as pages</param>
        public PaginatedTable(List<Table> pages)
        {
            Pages = pages;
        }

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
        public IEnumerator<Table> GetEnumerator()
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
