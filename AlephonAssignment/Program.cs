using AlephonAssignment.FileGenerator;
using System.Diagnostics;

namespace Program;
class Program
{
    public static void GenerateFast()
    {
        var g = new FileGenerator();
        g.GenerateFileFast("testspeed", 100000000, 10.0d, 30.0d);
        //g.SortFile("testspeed");
        //g.ShowFile("testspeed");
    }
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        GenerateFast();

        stopwatch.Stop();
        Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} milliseconds");
    }
}
