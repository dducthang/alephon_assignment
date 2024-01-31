using AlephonAssignment.FileGenerator;

namespace Program;
class Program
{
    static void Main(string[] args)
    {
        var g = new FileGenerator();
        g.GenerateFile("hellofile", 100, 40.0d, 50.0d);
        g.SortFile("hellofile");
    }
}
