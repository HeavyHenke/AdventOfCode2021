namespace AoC2021;

public class Day11
{
    public object A()
    {
        var rows = File.ReadLines("Day11.txt")
            .Select(r => r.Select(d => d - '0').ToArray()).ToArray();
        
        int numFlashes = 0;
        for (int day = 0; day < 100; day++)
        {
            var flashed = new HashSet<(int x, int y)>();
            for (int y = 0; y < rows.Length; y++)
            for (int x = 0; x < rows[y].Length; x++)
            {
                IncSquid(x, y, rows, flashed);
            }

            numFlashes += flashed.Count;
            foreach (var (x, y) in flashed)
                rows[y][x] = 0;
        }
        
        return numFlashes;
    }

    public object B()
    {
        var rows = File.ReadLines("Day11.txt")
            .Select(r => r.Select(d => d - '0').ToArray()).ToArray();
        
        for (int day = 0; day < Int32.MaxValue; day++)
        {
            var flashed = new HashSet<(int x, int y)>();
            for (int y = 0; y < rows.Length; y++)
            for (int x = 0; x < rows[y].Length; x++)
            {
                IncSquid(x, y, rows, flashed);
            }

            if (flashed.Count == 100)
                return day + 1;

            foreach (var (x, y) in flashed)
                rows[y][x] = 0;
        }

        throw new Exception("No flash for you");
    }

    private static void IncSquid(int x, int y, int[][] squids, HashSet<(int x, int y)> flashed)
    {
        squids[y][x]++;
        if (squids[y][x] > 9)
        {
            squids[y][x] = 0;
            flashed.Add((x, y));
            foreach (var n in GetNeighbours(x, y))
            {
                IncSquid(n.x, n.y, squids, flashed);
            }
        }
    }
    
    private static IEnumerable<(int x, int y)> GetNeighbours(int x, int y)
    {
        foreach(var dx in Enumerable.Range(-1, 3))
        foreach (var dy in Enumerable.Range(-1, 3))
        {
            if(dx == 0 && dy == 0)
                continue;
            var x2 = x + dx;
            var y2 = y + dy;
            if (x2 is >= 0 and < 10 && y2 is >= 0 and < 10)
                yield return (x2, y2);
        }
            
    }

}