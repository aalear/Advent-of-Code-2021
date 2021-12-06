var fish = new long[10];
foreach (var i in File.ReadAllLines("input.txt")[0].Split(",").Select(f => int.Parse(f)))
{
    fish[i]++;
}

// Part 1
for (var i = 0; i < 80; i++)
{
    Tick();
}
Console.WriteLine(fish.Sum());

// Part 2
for (var i = 80; i < 256; i++)
{
    Tick();
}
Console.WriteLine(fish.Sum());

// Helpers
void Tick()
{
    // Off by 1 here to give it the extra "day" - the loop below will put the new counts in the
    // right position
    fish[9] = fish[0];
    fish[7] += fish[0];
    fish[0] = 0;

    for (var j = 1; j < 10; j++)
    {
        if (fish[j] == 0)
            continue;

        fish[j - 1] = fish[j];
        fish[j] = 0;
    }
}