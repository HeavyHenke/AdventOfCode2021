using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;

namespace AoC2021
{
    class Day5
    {
        public object A()
        {
            var data = File.ReadLines("Day5.txt")
                .Select(l => l.Replace(" -> ", ","))
                .Select(l => l.Split(","))
                .Select(l => l.Select(int.Parse).ToArray())
                .Select(l => (x1: l[0], y1: l[1], x2: l[2], y2: l[3]));

            var map = new Dictionary<(int x, int y), int>();

            void AddPt(int x, int y)
            {
                if (map.TryGetValue((x, y), out var val) == false)
                    val = 0;
                map[(x, y)] = val + 1;
            }
            
            foreach (var line in data)
            {
                if (line.x1 == line.x2)
                {
                    var startY = Min(line.y1, line.y2);
                    var stopY = Max(line.y1, line.y2);
                    for (int y = startY; y <= stopY; y++)
                        AddPt(line.x1, y);
                }
                else if (line.y1 == line.y2)
                {
                    var startX = Min(line.x1, line.x2);
                    var stopX = Max(line.x1, line.x2);
                    for (int x = startX; x <= stopX; x++)
                        AddPt(x, line.y1);
                }
            }

            return map.Values.Count(v => v > 1);
        }

        public object B()
        {
            var data = File.ReadLines("Day5.txt")
                .Select(l => l.Replace(" -> ", ","))
                .Select(l => l.Split(","))
                .Select(l => l.Select(int.Parse).ToArray())
                .Select(l => (x1: l[0], y1: l[1], x2: l[2], y2: l[3]));

            var map = new Dictionary<(int x, int y), int>();

            foreach (var line in data)
            {
                int length = Max(Abs(line.x1 - line.x2), Abs(line.y1 - line.y2));
                int dx = line.x2.CompareTo(line.x1);
                int dy = line.y2.CompareTo(line.y1);
                for (int i = 0; i <= length; i++)
                {
                    int x = line.x1 + i * dx;
                    int y = line.y1 + i * dy;
                    if (map.TryGetValue((x, y), out var val) == false)
                        val = 0;
                    map[(x, y)] = val + 1;
                }
            }
            
            return map.Values.Count(v => v > 1);
        }
    }
}