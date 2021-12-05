using System;
using System.IO;
using System.Linq;
using MoreLinq.Extensions;

namespace AoC2021
{
    public class Day3
    {
        public object A()
        {
            var lines = File.ReadAllLines("Day3.txt");

            var mostCommon = lines
                .Transpose()
                .Select(t => t.Count(c => c == '1') > lines.Length / 2 ? '1' : '0')
                .ToArray();

            var gamma = Convert.ToInt32(new string(mostCommon), 2);
            var epsilon = ~gamma & (int)(Math.Pow(2, lines[0].Length) - 1);

            return gamma * epsilon;
        }

        public object B()
        {
            var originalLines = File.ReadAllLines("Day3.txt");

            var lines = originalLines;
            for (int pos = 0; pos < lines[0].Length && lines.Length != 1; pos++)
            {
                var num0 = lines.Select(l => l[pos]).Count(p => p == '0');
                var num1 = lines.Select(l => l[pos]).Count(p => p == '1');
                if (num1 > lines.Length/2 || num0 == num1)
                    lines = lines.Where(l => l[pos] == '1').ToArray();
                else
                    lines = lines.Where(l => l[pos] == '0').ToArray();
            }
            var oxyRating = Convert.ToInt32(lines[0], 2);

            lines = originalLines;
            for (int pos = 0; pos < lines[0].Length && lines.Length != 1; pos++)
            {
                var num0 = lines.Select(l => l[pos]).Count(p => p == '0');
                var num1 = lines.Select(l => l[pos]).Count(p => p == '1');
                if (num1 > lines.Length/2 || num0 == num1)
                    lines = lines.Where(l => l[pos] == '0').ToArray();
                else
                    lines = lines.Where(l => l[pos] == '1').ToArray();

                if (lines.Length == 1)
                    break;
            }
            var scrubbing = Convert.ToInt32(lines[0], 2);


            return oxyRating*scrubbing;
        }
    }
}