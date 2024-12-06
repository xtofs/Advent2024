namespace console.Extensions;

public static class StringExtensions
{


    public static IEnumerable<string> Lines(this string value)
    {
        var start = 0;
        var index = 0;
        while (index < value.Length)
        {
            switch (value[index])
            {
                case '\r':
                    yield return value[start..index];
                    if (index + 1 < value.Length && value[index + 1] == '\n')
                    {
                        index++;
                    }
                    start = index + 1;
                    break;
                case '\n':
                    yield return value[start..index]; ;
                    start = index + 1;
                    break;
            }
            index++;
        }
        if (start < value.Length)
        {
            yield return value[start..]; ;
        }
    }
}
