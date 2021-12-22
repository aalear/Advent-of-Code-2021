var input = File.ReadAllLines("input.txt");

const int PART1_STEPS = 100;
const int WIDTH = 10;
const int HEIGHT = 10;

var map = new Octopus[HEIGHT, WIDTH];

for (var y = 0; y < HEIGHT; y++)
{
    var line = input[y];
    for (var x = 0; x < WIDTH; x++)
    {
        map[y, x] = new Octopus(x, y, map, energyLevel: line[x] - '0');
    }
}

var totalFlashes = 0;
for(var i = 0; i < int.MaxValue; i++)
{
    for (var y = 0; y < HEIGHT; y++)
    {
        for (var x = 0; x < WIDTH; x++)
        {
            map[y, x].Tick();
        }
    }

    bool newFlash;
    var stepFlashes = 0;
    do
    {
        newFlash = false;
        for (var y = 0; y < HEIGHT; y++)
        {
            for (var x = 0; x < WIDTH; x++)
            {
                newFlash = map[y, x].TryFlash(out var flashes) || newFlash;
                if (i < PART1_STEPS)
                {
                    totalFlashes += flashes;
                }
                stepFlashes += flashes;
            }
        }
    } while (newFlash);

    if (stepFlashes == HEIGHT * WIDTH && i > PART1_STEPS)
    {
        Console.WriteLine($"(Part 2) All flashed at step {i + 1}");
        break;
    }

    StartNewStep();
};
Console.WriteLine($"(Part 1) {totalFlashes} total flashes in 100 steps");

void StartNewStep()
{
    for (var y = 0; y < HEIGHT; y++)
    {
        for (var x = 0; x < WIDTH; x++)
        {
            map[y, x].ResetStep();
        }
    }
}

class Octopus
{
    private readonly int _x;
    private readonly int _y;
    private readonly Octopus[,] _map;
    private bool _flashed;
    private int _energyLevel;

    public Octopus(int x, int y, Octopus[,] map, int energyLevel)
    {
        _x = x;
        _y = y;
        _map = map;
        _energyLevel = energyLevel;
    }

    public void Tick()
    {
        if (_flashed)
            return;

        _energyLevel++;
    }

    public bool TryFlash(out int flashCount)
    {
        if (_energyLevel > 9)
        {
            _flashed = true;
            _energyLevel = 0;

            var neighborFlashes = 0;
            foreach(var neighbor in GetNeighbors())
            {
                neighbor.Tick();
                neighbor.TryFlash(out var flashes);
                neighborFlashes += flashes;
            }

            flashCount = 1 + neighborFlashes;
            return true;
        }

        flashCount = 0;
        return false;
    }

    public void ResetStep() => _flashed = false;

    private IEnumerable<Octopus> GetNeighbors()
    {
        var height = _map.GetLength(0);
        var width = _map.GetLength(1);

        var hasTop = _y > 0;
        var hasLeft = _x > 0;
        var hasRight = _x < width - 1;
        var hasBottom = _y < height - 1;

        if (hasTop)
        {
            yield return _map[_y - 1, _x];

            if (hasLeft)
                yield return _map[_y - 1, _x - 1];
            if (hasRight)
                yield return _map[_y - 1, _x + 1];
        }

        if (hasLeft)
            yield return _map[_y, _x - 1];

        if (hasRight)
            yield return _map[_y, _x + 1];

        if (hasBottom)
        {
            yield return _map[_y + 1, _x];

            if (hasLeft)
                yield return _map[_y + 1, _x - 1];
            if (hasRight)
                yield return _map[_y + 1, _x + 1];
        }
    }
}