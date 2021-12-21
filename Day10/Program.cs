var closeToOpenChars = new Dictionary<char, char>
{
    { ')', '(' },
    { ']', '[' },
    { '}', '{' },
    { '>', '<' },
};
var openToCloseChars = closeToOpenChars.ToDictionary(i => i.Value, i => i.Key);

var values_part1 = new Dictionary<char, int>
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 }
};
var values_part2 = new Dictionary<char, int>
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 }
};
var illegalLineSum = 0; // Part 1
var autocompleteSums = new List<long>(); // Part 2
foreach (var line in File.ReadAllLines("input.txt"))
{
    var stack = new Stack<char>();
    var discardLine = false;
    foreach (var c in line)
    {
        if (openToCloseChars.ContainsKey(c))
        {
            stack.Push(c);
        }
        else
        {
            var top = stack.Peek();
            if (top != closeToOpenChars[c])
            {
                illegalLineSum += values_part1[c];
                discardLine = true;
                break;
            }
            else
            {
                stack.Pop();
            }
        }
    }

    // Part 2
    if (!discardLine && stack.Count > 0)
    {
        var sum = (long)0;
        while (stack.TryPop(out var c))
        {
            sum = sum * 5 + values_part2[openToCloseChars[c]];
        }
        autocompleteSums.Add(sum);
    }
}
// Part 1
Console.WriteLine(illegalLineSum);

// Part 2
autocompleteSums = autocompleteSums.OrderBy(s => s).ToList();
Console.WriteLine(autocompleteSums[autocompleteSums.Count / 2]);
