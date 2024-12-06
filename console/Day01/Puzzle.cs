using System.Diagnostics;

namespace console.Day01;


public class Puzzle : IPuzzle
{
    public string SolvePart1(string input)
    {
        return $"distance {input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                       .Select(parts => (left: int.Parse(parts[0]), right: int.Parse(parts[1])))
                       .OrderBy(pair => pair.left)
                       .Zip(input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                         .Select(parts => int.Parse(parts[1]))
                         .OrderBy(right => right),
                        (left, right) => Math.Abs(left.left - right))
                       .Sum()}";
    }


    public string SolvePart2(string input)
    {
        var left = new List<int>();
        var right = new Dictionary<int, int>();
        foreach (var line in input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(parts[0]));

            var r = int.Parse(parts[1]);
            right[r] = 1 + (right.TryGetValue(r, out var val) ? val : 0);
        }

        int score = 0;
        foreach (var l in left)
        {

            score += l * right.GetValueOrDefault(l, 0);
        }

        return $"score {score}";
    }


}
