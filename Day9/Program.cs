var rows = File.ReadAllLines("input.txt");

// We'll be padding with -1s on all sides to make the loop easier
var width = rows[0].Length + 2;
var height = rows.Length + 2;
var map = new int[height, width];

for(var i = 0; i < height; i++)
{
    var row = i > 0 && i < height - 1 ? rows[i - 1] : null;
    for(var j = 0; j < width; j++)
    {
        if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
        {
            map[i, j] = -1;
        }
        else
        {
            map[i, j] = row[j - 1] - '0';
        }
    }
}

// Part 1
var lowPoints = new List<int>();
for (var i = 1; i < height - 1; i++)
{
    for (var j = 1; j < width - 1; j++)
    {
        var pt = map[i, j];
        var top = map[i - 1, j];
        var right = map[i, j + 1];
        var left = map[i, j - 1];
        var bottom = map[i + 1, j];

        List<bool> checkResults = new();
        checkResults.Add(IsLarger(pt, top));
        checkResults.Add(IsLarger(pt, right));
        checkResults.Add(IsLarger(pt, left));
        checkResults.Add(IsLarger(pt, bottom));

        if (checkResults.All(r => !r))
        {
            lowPoints.Add(pt);
        }
    }
}
Console.WriteLine(lowPoints.Sum(i => i + 1));

static bool IsLarger(int value, int neighbor) => neighbor != -1 && neighbor <= value;