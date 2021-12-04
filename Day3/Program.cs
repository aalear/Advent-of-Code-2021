var input = File.ReadAllLines("input.txt").ToList();

// Part 1
string gamma = "", epsilon = "";
for (var i = 0; i < input[0].Length; i++)
{
    var mostCommon = GetMostCommonValue(input, i);

    gamma += mostCommon == "1" ? "1" : "0";
    epsilon += mostCommon == "1" ? "0" : "1";
}
Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));

// Part 2
string oxygen = "", co2 = "";
for (var i = 0; i < input[0].Length; i++)
{
    var oxygenCandidates = input.Where(s => s.StartsWith(oxygen));
    if(oxygenCandidates.Count() > 1) 
    {
        oxygen += GetMostCommonValue(oxygenCandidates, i) == "1" ? "1" : "0";
    }
    else
    {
        oxygen = oxygenCandidates.Single();
    }

    var co2candidates = input.Where(s => s.StartsWith(co2));
    if (co2candidates.Count() > 1)
    {
        co2 += GetMostCommonValue(co2candidates, i) == "1" ? "0" : "1";
    }
    else
    {
        co2 = co2candidates.Single();
    }
}
Console.WriteLine(Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2));

// Helpers
static string GetMostCommonValue(IEnumerable<string> readings, int position)
{
    var readingsInPosition = readings.Select(r => r[position]);
    return readingsInPosition.Count(c => c == '1') >= readingsInPosition.Count(c => c == '0') ? "1" : "0";
}