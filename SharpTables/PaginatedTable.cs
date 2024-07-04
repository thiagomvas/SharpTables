using System.Collections;

namespace SharpTables
{
    /// <summary>
    /// Encapsulates a list of tables and provides methods to navigate through them. Implements IEnumerable to allow foreach iteration.
    /// </summary>
    public class PaginatedTable : IEnumerable<Table>
    {
        private int _currentPage = 0;
        /// <summary>
        /// Gets the current page number. Will never go out of bounds.
        /// </summary>
        public int CurrentPage
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

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages => Pages.Count;

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>

        public List<Table> Pages;

        /// <summary>
        /// Creates a new instance of PaginatedTable with the given pages.
        /// </summary>
        /// <param name="pages">The tables to be used as pages</param>
        public PaginatedTable(List<Table> pages)
        {
            this.Pages = pages;
        }

        /// <summary>
        /// Prints the page at the given index.
        /// </summary>
        /// <param name="page"></param>
        public void PrintPage(int page)
        {
            if (page < 0 || page > TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            Pages[page].Print();
        }

        /// <summary>
        /// Prints the current page.
        /// </summary>
        public void PrintCurrentPage()
        {
            PrintPage(CurrentPage);
        }

        /// <summary>
        /// Prints the next page.
        /// </summary>
        public void NextPage()
        {
            if (CurrentPage < TotalPages - 1)
            {
                CurrentPage++;
                PrintCurrentPage();
            }
        }

        /// <summary>
        /// Prints the previous page.
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentPage >= 1)
            {
                CurrentPage--;
                PrintCurrentPage();
            }
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
