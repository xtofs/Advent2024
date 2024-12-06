namespace console;

using System.Diagnostics;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        if (Debugger.IsAttached)
        {
            Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "..", "..", "..");
        }

        var action = args.Length > 0 ? args[0] : "latest";

        switch (action.ToLower())
        {
            case "latest":
                RunLatestDay();
                break;
            case "single":
                var day = args.Length > 1 ? int.Parse(args[1]) : 1;
                RunSingleDay(day);
                break;
            default:
                Console.WriteLine("Invalid action specified.");
                break;
        }
    }

    private static void RunLatestDay()
    {
        var latestPuzzleDay = LatestImplmentedPuzzleDay();
        RunSingleDay(latestPuzzleDay);
    }

    private static void RunSingleDay(int day)
    {
        var puzzle = CreatePuzzleInstance(day);

        var dayStr = day.ToString().PadLeft(2, '0');

        foreach (var (test, part) in new[] { (true, 1), (false, 1), (true, 2), (false, 2) })
        {
            Console.WriteLine($"## Day {day} Part {part} {(test ? "Test" : "Solution")}: Running...");

            var inputFilePath = test ? $"Day{dayStr}/test.txt" : $"Day{dayStr}/input.txt";

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Day {day} Part {part} {(test ? "Test" : "Solution")}: No {(test ? "test" : "puzzle")} input file found.");
                continue;
            }

            var input = File.ReadAllText(inputFilePath);
            Func<string, string> solve = part == 1 ? puzzle.SolvePart1 : puzzle.SolvePart2;

            var result = solve(input);
            Console.WriteLine($"Result : {result}");
            Console.WriteLine();
        }
    }

    private static int LatestImplmentedPuzzleDay()
    {
        var all = Assembly
            .GetExecutingAssembly()?
            .GetTypes()?
            .Where(t => t.GetInterfaces().Contains(typeof(IPuzzle)))
            ?? throw new Exception("No puzzle classes found.");

        var latest = all.Select(t => t.Namespace!.Substring(t.Namespace.Length - 2, 2)).Select(int.Parse).Max();
        return latest;
    }

    private static IPuzzle CreatePuzzleInstance(int day)
    {
        var dayStr = day.ToString().PadLeft(2, '0');

        var fullName = $"console.Day{dayStr}.Puzzle";
        var type = Assembly
            .GetExecutingAssembly()?
            .GetTypes()?
            .FirstOrDefault(t => t.FullName == fullName) ?? throw new Exception($"Puzzle class for Day {dayStr} not found. please create a class named Puzzle implementing IPuzzle in namespace console..Day{dayStr}.");

        return (Activator.CreateInstance(type) as IPuzzle)!;
    }
}
