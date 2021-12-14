namespace AoC2021;

public class Day13
{
    public object A()
    {
        var lines = File.ReadAllLines("Day13.txt");

        HashSet<(int x, int y)> dots = new();

        int ix = 0;
        while (ix < lines.Length)
        {
            if (lines[ix] == "")
                break;
            var parts = lines[ix].Split(',');
            dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            ix++;
        }

        int maxX = dots.Max(d => d.x);
        int maxY = dots.Max(d => d.y);

        ix++;
        while (ix < lines.Length)
        {
            var parsed = lines[ix].Replace("fold along ", "");
            var val = int.Parse(parsed.Split('=').Last());
            if (parsed.StartsWith("y"))
            {
                var y1 = val - 1;
                var y2 = val + 1;
                while (y1 >= 0)
                {
                    for (int x = 0; x <= maxX; x++)
                    {
                        if (dots.Remove((x, y2))) 
                            dots.Add((x, y1));
                    }

                    y1--;
                    y2++;
                }
            }
            else
            {
                var x1 = val - 1;
                var x2 = val + 1;
                while (x1 >= 0)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        if (dots.Remove((x2, y)))
                            dots.Add((x1, y));
                    }

                    x1--;
                    x2++;
                }
            }

            ix++;
            break;
        }

        //Print(dots);

        return dots.Count;
    }
    
    public object B()
    {
        var lines = File.ReadAllLines("Day13.txt");

        HashSet<(int x, int y)> dots = new();

        int ix = 0;
        while (ix < lines.Length)
        {
            if (lines[ix] == "")
                break;
            var parts = lines[ix].Split(',');
            dots.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            ix++;
        }


        ix++;
        while (ix < lines.Length)
        {
            int maxX = dots.Max(d => d.x);
            int maxY = dots.Max(d => d.y);
            int minX = dots.Min(d => d.x);
            int minY = dots.Min(d => d.y);
            
            Console.WriteLine(lines[ix]);
            var parsed = lines[ix].Replace("fold along ", "");
            var val = int.Parse(parsed.Split('=').Last());
            if (parsed.StartsWith("y"))
            {
                var y1 = val - 1;
                var y2 = val + 1;
                while (y1 >= minY || y2 <= maxY)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        if (dots.Remove((x, y2))) 
                            dots.Add((x, y1));
                    }

                    y1--;
                    y2++;
                }
            }
            else
            {
                var x1 = val - 1;
                var x2 = val + 1;
                while (x1 >= minX || x2 <= maxX)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        if (dots.Remove((x2, y)))
                            dots.Add((x1, y));
                    }

                    x1--;
                    x2++;
                }
            }

            ix++;
        }

        Print(dots);
        return "HZKHFEJZ";
    }


    private static void Print(HashSet<(int x, int y)> dots)
    {
        var minX = dots.Min(d => d.x);
        var maxX = dots.Max(d => d.x);
        var minY = dots.Min(d => d.y);
        var maxY = dots.Max(d => d.y);

        Console.WriteLine($"Total dots {dots.Count}");
        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if(dots.Contains((x,y)))
                    Console.Write('#');
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
    }
}