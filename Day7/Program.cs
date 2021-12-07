var xPos = File.ReadAllLines("input.txt")[0].Split(",").Select(n => int.Parse(n));

// Part 1
var fuel = xPos.Sum(n => Math.Abs(Median(xPos) - n));
Console.WriteLine(fuel);

// Part 2
// I got the "this is a series with the n*(n+1)/2 formula for the nth value" part, but I don't know how to get the destination coordinate.
// xPos.Average() is not it. So... checking all possibilities it is, I guess.
fuel = double.MaxValue;
foreach(var x in Enumerable.Range(xPos.Min(), xPos.Max()))
{
    var f = xPos.Sum(n => (Math.Abs(x - n) * (Math.Abs(x - n) + 1) / 2));

    if(f < fuel)
    {
        fuel = f;
    }
}
Console.WriteLine(fuel);

static double Median(IEnumerable<int> values)
{
    var numbers = values.OrderBy(n => n);
    var midpoint = numbers.Count() / 2;

    return numbers.Count() % 2 == 0
        ? (numbers.ElementAt(midpoint - 1) + numbers.ElementAt(midpoint)) / 2.0
        : numbers.ElementAt(midpoint);
}