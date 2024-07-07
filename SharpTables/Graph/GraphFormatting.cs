namespace SharpTables.Graph
{
    public record GraphFormatting
    {
        public char EmptyPoint { get; set; } = ' ';
        public char HorizontalLine { get; set; } = '-';
        public char VerticalLine { get; set; } = '|';
        public char Origin { get; set; } = 'O';
        public char XAxisTick { get; set; } = '+';
        public char YAxisTick { get; set; } = '+';
        public char YAxisTickLine { get; set; } = '.';
        public char GraphIcon { get; set; } = '#';

        public ConsoleColor YAxisColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor XAxisColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor GraphIconColor { get; set; } = ConsoleColor.Yellow;
        public ConsoleColor EmptyPointColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor YAxisTickLineColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor YAxisLabelColor { get; set; } = ConsoleColor.White;
        public ConsoleColor XAxisLabelColor { get; set; } = ConsoleColor.White;
        public ConsoleColor XAxisTickColor { get; set; } = ConsoleColor.White;


    }
}
