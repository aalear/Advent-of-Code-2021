var lines = File.ReadAllLines("input.txt");

// Part 1
var segmentLengths = new[] { 2, 3, 4, 7 }; // for numbers 1, 7, 4, and 8
Console.WriteLine(lines.SelectMany(line => line.Split(" | ")[1].Split(" ")).Count(word => segmentLengths.Contains(word.Length)));

// Part 2
var sum = 0;
foreach (var line in lines)
{
    // https://stackoverflow.com/a/69905509
    (var input, var output) = line.Split(" | ") switch { var i => (i[0].Split(" "), i[1].Split(" ")) };

    var one = input.First(i => i.Length == 2);
    var four = input.First(i => i.Length == 4);
    var seven = input.First(i => i.Length == 3);

    int Identify(string segment)
    {
        switch(segment.Length)
        {
            case 2: return 1;
            case 3: return 7;
            case 4: return 4;
            case 5:
                if (one.Intersect(segment).Count() == 2)
                {
                    return 3;
                }
                if (four.Intersect(segment).Count() == 2)
                {
                    return 2;
                }
                return 5;
            case 6:
                if (one.Intersect(segment).Count() == 1)
                {
                    return 6;
                }
                if (four.Intersect(segment).Count() == 4)
                {
                    return 9;
                }
                return 0;
            case 7: return 8;
            default: throw new InvalidOperationException($"Unsupported segment length found: {segment.Length}");
        }
    }

    for (var i = 0; i < 4; i++) 
    {
        sum += Convert.ToInt32(Identify(output[i])) * (int)Math.Pow(10, 3 - i);
    }
}
Console.WriteLine(sum);