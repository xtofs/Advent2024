namespace console.Day02;


public class Puzzle : IPuzzle
{
    public string SolvePart1(string input)
    {
        var totalSafeReports = 0;

        // go through for each line -> create a list
        // check the first two items
        // -> bool: increasing/decreasing
        // -> bool: safe/unsafe
        // check remaining pairs
        // once at end, add to totalSafeReports if safe

        // parse input, split based on \n
        foreach (var line in input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var difference = int.Parse(parts[1]) - int.Parse(parts[0]);
            bool safe = Math.Abs(difference) >= 1 && Math.Abs(difference) <= 3;
            bool increasing = difference > 0;


            for (int i = 2; i < parts.Length && safe; i++)
            {
                var nextDifference = int.Parse(parts[i]) - int.Parse(parts[i - 1]);
                var nextIncreasing = nextDifference > 0;

                bool consistentIncreaseOrDecrease = increasing == nextIncreasing;
                bool slightDifference = Math.Abs(nextDifference) >= 1 && Math.Abs(nextDifference) <= 3;

                safe = consistentIncreaseOrDecrease && slightDifference;

                // Console.WriteLine($"{nextDifference} {nextIncreasing} {consistentIncreaseOrDecrease} {safe}");
            }

            if (safe)
            {
                totalSafeReports++;
                // Console.WriteLine($"Safe report: {line}");
            }
        }

        return $"Safe reports {totalSafeReports}";
    }

    public string SolvePart2(string input)
    {
        var result = 0;

        return $"tolerable reports {result}";


    }

}
