var input = File.ReadAllLines("input.txt");

var template = input[0];
var pairMappings = input.Skip(2).Select(s => s.Split(" -> ")).ToDictionary(p => p[0], p => p[1]);

for (var i = 0; i < 10; i++)
{
    template = Insert(template);
}
PrintResult("Part 1: ", template);

string Insert(string template)
{
    for (var i = 0; i < template.Length - 1; i++)
    {
        var pair = template.Substring(i, 2);

        if(pairMappings.ContainsKey(pair))
        {
            template = template.Insert(i + 1, pairMappings[pair]);
            i++;
        }
    }

    return template;
}

void PrintResult(string prefix, string data)
{
    var letterCounts = data.GroupBy(c => c).Select(g => g.ToList().Count).OrderBy(n => n);
    Console.WriteLine($"{prefix}{letterCounts.Last() - letterCounts.First()}");
}