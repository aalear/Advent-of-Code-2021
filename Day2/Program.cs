var input = File.ReadAllLines("input.txt").Select(l => l.Trim().Split(" "));

int position = 0;
int depth = 0;

// Part 1
var directions = input.ToLookup(i => i[0], i => int.Parse(i[1]));
var forwards = directions["forward"];
foreach (var i in forwards)
    position += i;

var ups = directions["up"];
foreach (var i in ups)
    depth -= i;

var downs = directions["down"];
foreach (var i in downs)
    depth += i;
Console.WriteLine(position * depth);

// Part 2
position = depth = 0;
var aim = 0;
foreach (var i in input)
{
    var val = int.Parse(i[1]);
    switch (i[0])
    {
        case "forward":
            position += val;
            depth += aim * val;
            break;
        case "down":
            aim += val;
            break;
        case "up":
            aim -= val;
            break;
    }
}
Console.WriteLine(position * depth);