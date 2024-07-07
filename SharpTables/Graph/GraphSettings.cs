namespace SharpTables.Graph
{
    public record GraphSettings<T>
    {
        public Func<T, double> ValueGetter { get; init; } = x => (double)(object)x;
        public Func<T, string> XTickFormatter { get; init; } = x => x.ToString();
        public Func<double, string> YTickFormatter { get; init; } = y => y.ToString();
        public int YAxisPadding { get; init; } = 1;
        public int XAxisPadding { get; init; } = 1;
        public int NumOfYTicks { get; init; } = 5;

    }
}
