using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AoC2021;

public class Day22
{
    public object A()
    {
        var commands = new List<(bool state, int x1, int x2, int y1, int y2, int z1, int z2)>();
        foreach (var cmdRow in File.ReadLines("Day22.txt"))
        {
            var m = Regex.Match(cmdRow, @"^(on|off) x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)");
            bool state = m.Groups[1].Value == "on";
            var x1 = int.Parse(m.Groups[2].Value);
            var x2 = int.Parse(m.Groups[3].Value);
            if (x1 > 50 || x2 < -50)
                continue;
            x1 = Math.Max(x1, -50);
            x2 = Math.Min(x2, 50);

            var y1 = int.Parse(m.Groups[4].Value);
            var y2 = int.Parse(m.Groups[5].Value);
            if (y1 > 50 || y2 < -50)
                continue;
            y1 = Math.Max(y1, -50);
            y2 = Math.Min(y2, 50);

            var z1 = int.Parse(m.Groups[6].Value);
            var z2 = int.Parse(m.Groups[7].Value);
            if (z1 > 50 || z2 < -50)
                continue;
            z1 = Math.Max(z1, -50);
            z2 = Math.Min(z2, 50);

            commands.Add((state, x1, x2, y1, y2, z1, z2));
        }

        var onCubes = new HashSet<(int x, int y, int z)>();

        foreach (var cmd in commands)
        {
            for (int x = cmd.x1; x <= cmd.x2; x++)
            for (int y = cmd.y1; y <= cmd.y2; y++)
            for (int z = cmd.z1; z <= cmd.z2; z++)
            {
                if (cmd.state)
                    onCubes.Add((x, y, z));
                else
                    onCubes.Remove((x, y, z));
            }
        }

        return onCubes.Count;
    }

    public object B()
    {
        var commands = new List<(bool state, long x1, long x2, long y1, long y2, long z1, long z2)>();
        foreach (var cmdRow in File.ReadLines("Day22.txt"))
        {
            var m = Regex.Match(cmdRow, @"^(on|off) x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)");
            bool state = m.Groups[1].Value == "on";
            var x1 = long.Parse(m.Groups[2].Value);
            var x2 = long.Parse(m.Groups[3].Value);
            // if (x1 > 50 || x2 < -50)
            //     continue;
            // x1 = Math.Max(x1, -50);
            // x2 = Math.Min(x2, 50);
            
            var y1 = long.Parse(m.Groups[4].Value);
            var y2 = long.Parse(m.Groups[5].Value);
            // if (y1 > 50 || y2 < -50)
            //     continue;
            // y1 = Math.Max(y1, -50);
            // y2 = Math.Min(y2, 50);
            
            var z1 = long.Parse(m.Groups[6].Value);
            var z2 = long.Parse(m.Groups[7].Value);
            // if (z1 > 50 || z2 < -50)
            //     continue;
            // z1 = Math.Max(z1, -50);
            // z2 = Math.Min(z2, 50);
            
            commands.Add((state, x1, x2, y1, y2, z1, z2));
        }

        // All coords below is inclusive start exclusive end 
        var xCoords = commands.Select(c => c.x1).Concat(commands.Select(c => c.x2+1)).Distinct().OrderBy(x => x).ToList();
        var yCoords = commands.Select(c => c.y1).Concat(commands.Select(c => c.y2+1)).Distinct().OrderBy(y => y).ToList();
        var zCoords = commands.Select(c => c.z1).Concat(commands.Select(c => c.z2+1)).Distinct().OrderBy(z => z).ToList();

        long numCuboids = (xCoords.Count - 1) * (yCoords.Count - 1) * (zCoords.Count - 1);
        long cuboidNum = 0;
        
        long numOn = 0;

        for (var xi = 0; xi < xCoords.Count - 1; xi++)
        for (var yi = 0; yi < yCoords.Count - 1; yi++)
        for (var zi = 0; zi < zCoords.Count - 1; zi++)
        {
            cuboidNum++;
            if((cuboidNum&0xFFFFF) == 0)
                Console.WriteLine($"{cuboidNum*100f/numCuboids}%");
            
            var x = xCoords[xi];
            var y = yCoords[yi];
            var z = zCoords[zi];
            var cubieSize = (xCoords[xi + 1] - xCoords[xi]) * (yCoords[yi + 1] - yCoords[yi]) * (zCoords[zi + 1] - zCoords[zi]);
            bool isOn = false;
            for (int cix = 0; cix < commands.Count; cix++)
            {
                var command = commands[cix];
                if (x >= command.x1 && x <= command.x2 && 
                    y >= command.y1 && y <= command.y2 && 
                    z >= command.z1 && z <= command.z2)
                    isOn = commands[cix].state;
            }

            if (isOn)
                numOn += cubieSize;
        }
        
        return numOn;   // Tog lite lång tid kanske (347 sekunder), vore noge bättre men en rekursiv algoritm som utgår från kuberna i respektive command.
    }
}