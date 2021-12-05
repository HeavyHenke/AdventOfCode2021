using System.IO;
using System.Linq;

class Day1
{
    public object A()
    {
        var depths = File.ReadAllLines("Day1.txt").Select(int.Parse);

        bool first = true;
        int last = -1;
        int numInc = 0;
        foreach (var depth in depths)
        {
            if (first)
            {
                first = false;
            }
            else if (depth > last)
            {
                numInc++;
            }

            last = depth;
        }

        return numInc;
    }
    
    public object B()
    {
        var depths = File
            .ReadAllLines("Day1.txt")
            .Select(int.Parse)
            .ToList();

        int numInc = 0;
        for (int i = 3; i < depths.Count; i++)
        {
            if (depths[i] > depths[i - 3])  // depths[i-1] and depths[i-2] is common for both sliding windows.
                numInc++;
        }
        
        return numInc;
    }
}