using SharpTables.Graph;
using System.Text;

var graph = new Graph<double>()
{
    Values = [10.1, 6.3, 20.12345, 1, 17.1, 11.9, 12, 19.361, 9.123]
};

var settings = new GraphSettings<double>()
{
    ValueGetter = x => x,
    XTickFormatter = x => RandomString(),
    YTickFormatter = y => y.ToString("0.0"),
    YAxisPadding = 2,
    XAxisPadding = 1,
    NumOfYTicks = 5
};


graph.Settings = settings;

graph.Write();
Console.WriteLine();

static char RandomChar()
{
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var random = new Random();
    return chars[random.Next(chars.Length)];
}

static string RandomString()
{
    var random = new Random();
    var str = new StringBuilder();
    var len = random.Next(3, 5);
    for (int i = 0; i < len; i++)
    {
        str.Append(RandomChar());
    }
    return str.ToString();
}