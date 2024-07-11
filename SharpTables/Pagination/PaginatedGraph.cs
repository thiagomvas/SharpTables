using SharpTables.Graph;
using System.Collections;
using System.Xml;

namespace SharpTables.Pagination
{
    public class PaginatedGraph<T> : IPagination<Graph<T>>
    {
        private List<T> values = new();

        private GraphSettings<T> _settings;
        private GraphFormatting _formatting;

        public GraphSettings<T> Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                Pages.ForEach(g => g.Settings = value);
            }
        }

        public GraphFormatting Formatting
        {
            get => _formatting;
            set
            {
                _formatting = value;
                Pages.ForEach(g => g.Formatting = value);
            }
        }

        private int _columnsPerPage = 7;
        /// <inheritdoc/>
        public int ColumnsPerPage
        {
            get => _columnsPerPage;
            set
            {
                _columnsPerPage = value;
                Paginate();
            }
        }

        /// <inheritdoc/>
        public int CurrentPageIndex { get; private set; } = 0;

        /// <inheritdoc/>
        public int TotalPages => Pages.Count;

        /// <inheritdoc/>
        public Graph<T> Current => Pages[CurrentPageIndex];

        /// <inheritdoc/>
        public List<Graph<T>> Pages { get; set; }

        public PaginatedGraph(IEnumerable<T> values)
        {
            this.values = values.ToList();
            Paginate();
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
        public IEnumerator<Graph<T>> GetEnumerator()
        {
            return Pages.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private void Paginate()
        {
            Pages = new();
            var chunks = values.Chunk(ColumnsPerPage);
            foreach (var chunk in chunks)
            {
                var graph = new Graph<T>(chunk.ToList());
                graph.Settings = Settings;
                graph.Formatting = Formatting;

                Pages.Add(graph);
            }
        }
    }
}
