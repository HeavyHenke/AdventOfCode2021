using System.Text.RegularExpressions;

namespace AoC2021;

public class Day17
{
    public object A()
    {
        var input = "target area: x=253..280, y=-73..-46";
        var m = Regex.Match(input, @"target area: x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");

        var xTargetStart = int.Parse(m.Groups[1].Value);
        var xTargetStop = int.Parse(m.Groups[2].Value);
        var yTargetStop = int.Parse(m.Groups[3].Value);
        var yTargetStart = int.Parse(m.Groups[4].Value);

        var hitX = GetXThatHits(xTargetStart, xTargetStop);
        var yVeloRange = GetYVeloRange(yTargetStart, yTargetStop);
        int maxY = int.MinValue;
        foreach (var x in hitX)
        {
            for (int y = yVeloRange.minYVelo; y <= yVeloRange.maxYVelo; y++)
            {
                var hit = IsHit(x, y, xTargetStart, xTargetStop, yTargetStart, yTargetStop);
                if (hit.wasHit && hit.maxHeight > maxY)
                    maxY = hit.maxHeight;
            }
        }
        
        return maxY;
    }
    
    public object B()
    {
        var input = "target area: x=253..280, y=-73..-46";
        var m = Regex.Match(input, @"target area: x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");

        var xTargetStart = int.Parse(m.Groups[1].Value);
        var xTargetStop = int.Parse(m.Groups[2].Value);
        var yTargetStop = int.Parse(m.Groups[3].Value);
        var yTargetStart = int.Parse(m.Groups[4].Value);

        var hitX = GetXThatHits(xTargetStart, xTargetStop);
        var yVeloRange = GetYVeloRange(yTargetStart, yTargetStop);
        int numHits = 0;
        foreach (var x in hitX)
        {
            for (int y = yVeloRange.minYVelo; y <= yVeloRange.maxYVelo; y++)
            {
                var hit = IsHit(x, y, xTargetStart, xTargetStop, yTargetStart, yTargetStop);
                if (hit.wasHit) 
                    numHits++;
            }
        }
        
        return numHits;
    }

    private static IEnumerable<int> GetXThatHits(int xTargetStart, int xTargetStop)
    {
        // x*(x+1)/2 >= xTartetStart
        // x^2+x-2*xTargetStart=0
        var p = 1f;
        var q = -2f * xTargetStart;
        var halfPSquared = (p / 2) * (p / 2);
        var root = Math.Sqrt(halfPSquared - q);
        var start = -p / 2 + root;
        var intStart = (int)Math.Ceiling(start);

        // More velocity that this overshoots in the first step
        var intStop = xTargetStop;

        return Enumerable.Range(intStart, intStop - intStart + 1);
    }

    private static (int minYVelo, int maxYVelo) GetYVeloRange(int yTargetStart, int yTargetStop)
    {
        // Shooting down
        var minVelo = yTargetStop;

        // Velo up = velo down @ y = 0
        var maxVelo = -yTargetStop;
        return (minVelo, maxVelo);
    }
    
    private static (bool wasHit, int maxHeight) IsHit(int veloX, int veloY, int xTargetStart, int xTargetStop, int yTargetStart, int yTargetStop)
    {
        int x = 0;
        int y = 0;
        int maxHeight = int.MinValue;
        while (true)
        {
            x += veloX;
            y += veloY;

            if (y > maxHeight)
                maxHeight = y;

            if (x >= xTargetStart && x <= xTargetStop && y >= yTargetStop && y <= yTargetStart)
                return (true, maxHeight);
            if (x > xTargetStop || y < yTargetStop)
                return (false, maxHeight);
            
            if (veloX > 0)
                veloX--;
            veloY--;
        }
    }
}