using System.Diagnostics;

namespace console.Day01;


public class Puzzle : IPuzzle
{
    public string SolvePart1(string input)
    {
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(parts[0]));
            right.Add(int.Parse(parts[1]));
        }
        Debug.Assert(left.Count == right.Count);

        left.Sort(); // TODO: Tyler to implement radix sort on List<int>
        right.Sort();

        var totalDistance = 0;
        for (int i = 0; i < left.Count; i++)
        {
            totalDistance += Math.Abs(left[i] - right[i]);
        }

        return $"distance {totalDistance}";
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
