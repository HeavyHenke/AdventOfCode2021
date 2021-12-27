namespace AoC2021;

public class Day20
{
    public object A()
    {
        var lines = File.ReadAllLines("Day20.txt");
        var enhancer = new bool[512];
        for (int i = 0; i < 512; i++)
        {
            if (lines[0][i] == '#')
                enhancer[i] = true;
        }

        var image = new HashSet<(int x, int y)>();
        for(int y =0; y < lines.Length-2;y++)
        for (int x = 0; x < lines[y+2].Length; x++)
        {
            if (lines[y+2][x] == '#')
                image.Add((x, y));
        }

        var i2 = Enhance(image, enhancer, false, true);
        var i3 = Enhance(i2, enhancer, true, false);
        PrintIt(i3);
        
        return i3.Count; 
    }

    public object B()
    {
        var lines = File.ReadAllLines("Day20.txt");
        var enhancer = new bool[512];
        for (int i = 0; i < 512; i++)
        {
            if (lines[0][i] == '#')
                enhancer[i] = true;
        }
        
        var image = new HashSet<(int x, int y)>();
        for(int y =0; y < lines.Length-2;y++)
        for (int x = 0; x < lines[y+2].Length; x++)
        {
            if (lines[y+2][x] == '#')
                image.Add((x, y));
        }

        bool reverse = false;
        for (int times = 0; times < 50; times++)
        {
            image = Enhance(image, enhancer, reverse, !reverse);
            reverse = !reverse;
        }

        return image.Count;
    }
    
    private static HashSet<(int x, int y)> Enhance(HashSet<(int x, int y)> image, bool[] enhancer, bool notInput, bool notOutput)
    {
        var minY = image.Min(i => i.y);
        var maxY = image.Max(i => i.y);
        var minX = image.Min(i => i.x);
        var maxX = image.Max(i => i.x);

        var result = new HashSet<(int x, int y)>();
        for (int y = minY - 1; y <= maxY + 1; y++)
        {
            for (int x = minX - 1; x <= maxX + 1; x++)
            {
                if (enhancer[GetValAtIx(image, x, y, notInput)] != notOutput)
                    result.Add((x, y));
            }
        }

        return result;
    }


    private static int GetValAtIx(HashSet<(int x, int y)> image, int x, int y, bool notOutput)
    {
        int val = 0;
        for(int dy = -1 ; dy < 2; dy++)
        for (int dx = -1; dx < 2; dx++)
        {
            val <<= 1;
            if (image.Contains((x + dx, y + dy)) != notOutput)
                val |= 1;
        }
        
        return val;
    }
    
    private static void PrintIt(HashSet<(int x, int y)> image)
    {
        var minY = image.Min(i => i.y);
        var maxY = image.Max(i => i.y);
        var minX = image.Min(i => i.x);
        var maxX = image.Max(i => i.x);
        
        Console.WriteLine($"y {minY}-{maxY}, x {minX}-{maxX}");

        for (int y = minY - 1; y <= maxY + 1; y++)
        {
            for (int x = minX - 1; x <= maxX + 1; x++)
            {
                Console.Write(image.Contains((x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }
        
        Console.WriteLine($"Num lit {image.Count}, num unlit {(maxX-minX)*(maxY-minY)-image.Count}");
    }
}