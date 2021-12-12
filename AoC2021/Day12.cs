namespace AoC2021;

public class Day12
{
    public object A()
    {
        var paths = File.ReadLines("Day12.txt")
            .Select(l => l.Split('-'))
            .SelectMany(l => new[] { l, new[] { l[1], l[0] } })
            .ToLookup(key => key[0]);

        int numPaths = 0;
        var stack = new Stack<(HashSet<string> visited, string isNow)>();
        stack.Push((new HashSet<string> { "start" }, "start"));

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            foreach (var way in paths[node.isNow])
            {
                if (way[1] == "end")
                {
                    numPaths++;
                    //Console.WriteLine($"Found path: {string.Join(", ", node.visited)}");
                }
                else if (char.IsUpper(way[1][0]) || node.visited.Contains(way[1]) == false)
                {
                    var h2 = new HashSet<string>(node.visited);
                    h2.Add(way[1]);
                    stack.Push((h2, way[1]));
                }
            }
        }

        return numPaths;
    }
    
    public object B()
    {
        var paths = File.ReadLines("Day12.txt")
            .Select(l => l.Split('-'))
            .SelectMany(l => new[] { l, new[] { l[1], l[0] } })
            .ToLookup(key => key[0]);

        int numPaths = 0;
        var stack = new Stack<(List<string> visited, string isNow, string smallCaveVisitedTwice)>();
        stack.Push((new List<string> { "start" }, "start", ""));

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            foreach (var way in paths[node.isNow])
            {
                if (way[1] == "start")
                    continue;
                
                if (way[1] == "end")
                {
                    numPaths++;
                }
                else if (char.IsUpper(way[1][0]) || node.visited.Contains(way[1]) == false || node.smallCaveVisitedTwice == "")
                {
                    var h2 = new List<string>(node.visited);

                    var smallCaveVisitedTwice = char.IsLower(way[1][0]) && h2.Contains(way[1]) ? way[1] : node.smallCaveVisitedTwice;
                    h2.Add(way[1]);

                    stack.Push((h2, way[1], smallCaveVisitedTwice));
                }
            }
        }

        return numPaths;
    }

}