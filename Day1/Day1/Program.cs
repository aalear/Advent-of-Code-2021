var input = File.ReadAllLines("input.txt").Select(n => int.Parse(n)).ToList();

var increasedCount = 0;
int previous = int.MaxValue;
foreach (var num in input)
{
    if (num > previous)
    {
        increasedCount++;
    }
    previous = num;
}
Console.WriteLine(increasedCount);

previous = input.Take(3).Sum();
increasedCount = 0;
for (var i = 1; i < input.Count - input.Count % 3; i++)
{
    var sum = input[i] + input[i + 1] + input[i + 2];

    if (sum > previous)
    {
        increasedCount++;
    }
    previous = sum;
}
Console.WriteLine(increasedCount);