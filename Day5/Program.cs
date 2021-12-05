var input = File.ReadAllLines("input.txt");

// Input parser
var points = new List<Point>();
foreach (var line in input)
{
    var endPoints = line.Split(" -> ")
                            .Select(p => { 
                                var values = p.Split(",").Select(v => int.Parse(v)).ToArray();
                                return new Point { x = values[0], y = values[1] }; 
                            })
                            .ToList();
    points.AddRange(endPoints);
}

// With 0-based indexing, the point (9, 9) for example, requires a 10x10 array
var maxX = points.Max(p => p.x) + 1;
var maxY = points.Max(p => p.y) + 1;

var grid = new int[maxX, maxY];

// Part 1
for (var i = 0; i < points.Count; i += 2) 
{
    var p1 = points[i];
    var p2 = points[i + 1];

    if (p1.x != p2.x && p1.y != p2.y)
        continue;

    RecordLine(p1, p2);
}
Console.WriteLine(CountMultilineOverlaps());

// Part 2
for (var i = 0; i < points.Count; i += 2)
{
    var p1 = points[i];
    var p2 = points[i + 1];

    if (p1.x == p2.x || p1.y == p2.y)
        continue;

    RecordLine(p1, p2);
}
Console.WriteLine(CountMultilineOverlaps());

void RecordLine(Point p1, Point p2)
{
    if (p1.y == p2.y)
    {
        for (var x = Math.Min(p1.x, p2.x); x <= Math.Max(p1.x, p2.x); x++)
        {
            grid[x, p1.y]++;
        }
    }
    else if(p1.x == p2.x)
    {
        for (var y = Math.Min(p1.y, p2.y); y <= Math.Max(p1.y, p2.y); y++)
        {
            grid[p1.x, y]++;
        }
    }
    else
    {
        // Starting at p1, advance both X and Y at the same time, accounting for 
        // the fact that p2 may have either axis value smaller than p1
        var xMod = p1.x < p2.x ? 1 : -1;
        var yMod = p1.y < p2.y ? 1 : -1;
        for (int x = p1.x, y = p1.y;
            x != p2.x + xMod && y != p2.y + yMod;
            x += xMod, y += yMod)
        {
            grid[x, y]++;
        }
    }
}

int CountMultilineOverlaps()
{
    var multiLineCrossCount = 0;
    for (var i = 0; i < maxX; i++)
    {
        for (var j = 0; j < maxY; j++)
        {
            if (grid[i, j] > 1)
            {
                multiLineCrossCount++;
            }
        }
    }
    return multiLineCrossCount;
}

struct Point
{
    public int x;
    public int y;
}