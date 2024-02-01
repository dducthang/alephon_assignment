using AlephonAssignment.FileGenerator;
using System.Diagnostics;

namespace Program;
class Program
{
    public static void GenerateBinFileFast()
    {
        var g = new FileGenerator();
        g.GenerateAndSaveRandomNumbers("testspeed", 1000000000, 10.0d, 30.0d);
    }
    public static void GenerateFast()
    {
        var g = new FileGenerator();
        g.GenerateAndSaveRandomNumbers("testspeed", 1000000000, 10.0d, 30.0d);
    }
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        GenerateBinFileFast();

        stopwatch.Stop();
        Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} milliseconds");
    }
}
