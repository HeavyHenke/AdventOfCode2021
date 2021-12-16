namespace AoC2021;

public class Day15
{
    public object A()
    {
        var lines = File.ReadAllLines("Day15.txt");

        var visited = new int[lines.Length][];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = new int[lines[0].Length];
            Array.Fill(visited[i], int.MaxValue);
        }
        
        int lowestRisk = int.MaxValue;
        var searchQueue = new Queue<(int x, int y, int risk)>();
        searchQueue.Enqueue((0, 0, 0));
        while (searchQueue.Count > 0)
        {
            var n = searchQueue.Dequeue();

            foreach( var (x,y) in GetNeighbours(n.x, n.y, lines[0].Length, lines.Length))
            {
                if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
                    continue;

                var risk = n.risk + lines[y][x] - '0';
                if (risk < visited[y][x])
                {
                    visited[y][x] = risk;
                    if (y == lines.Length - 1 && x == lines[y].Length - 1)
                        lowestRisk = Math.Min(risk, lowestRisk);
                    else
                        searchQueue.Enqueue((x, y, risk));
                }
            }
        }

        return lowestRisk;
    }

    private static IEnumerable<(int x, int y)> GetNeighbours(int x, int y, int width, int height)
    {
        if (x > 0)
            yield return (x - 1, y);
        if (y > 0)
            yield return (x, y-1);
        if(x < width-1)
            yield return (x + 1, y);
        if (y < height-1)
            yield return (x, y + 1);
    }

    public object B()
    {
        var lines = File.ReadAllLines("Day15.txt");

        var costs = new int[lines.Length * 5][];
        for(int yTimes = 0; yTimes < 5; yTimes++)
        for (int lineY = 0; lineY < lines.Length; lineY++)
        {
            costs[yTimes * lines.Length + lineY] = new int[lines[0].Length * 5];
            for (int xTimes = 0; xTimes < 5; xTimes++)
            for (int x = 0; x < lines[lineY].Length; x++)
            {
                var cost = lines[lineY][x] - '0' + yTimes + xTimes;
                cost = (cost - 1) % 9 + 1;
                costs[yTimes * lines.Length + lineY][xTimes * lines[lineY].Length + x] = cost;
            }
        }

        var visited = new int[lines.Length*5][];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = new int[lines[0].Length*5];
            Array.Fill(visited[i], int.MaxValue);
        }

        var searchList = new SortedSet<(int risk, int x, int y)> { (0, 0, 0) };

        while (searchList.Count > 0)
        {
            var n = searchList.First();
            searchList.Remove(n);

            foreach( var (x,y) in GetNeighbours(n.x, n.y, costs[0].Length, costs.Length))
            {
                var risk = n.risk + costs[y][x];
                if (risk < visited[y][x])
                {
                    visited[y][x] = risk;
                    if (y == costs.Length - 1 && x == costs[y].Length - 1)
                    {
                        return risk;
                    }

                    searchList.Add((risk, x, y));
                }
            }
        }

        throw new Exception("Not found");
    }
}