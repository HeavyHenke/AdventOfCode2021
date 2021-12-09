using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021
{
    public class Day8
    {
        /*   aaaa
            b    c
            b    c
             dddd
            e    f
            e    f
             gggg
         */

        private static readonly Dictionary<string, int> AllNumbers = new()
        {
            {"abcefg", 0}, {"cf", 1}, {"acdeg", 2},
            {"acdfg", 3}, {"bcdf", 4}, {"abdfg", 5}, {"abdefg", 6},
            {"acf", 7}, {"abcdefg", 8}, {"abcdfg", 9}
        };
        
        public object A()
        {
            var data = File.ReadAllLines("Day8.txt");

            var total1478 = 0;
            foreach (var d in data)
            {
                var resultRow = d.Split('|')[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                total1478 += resultRow.Count(r => r.Length is 2 or 3 or 4 or 7);
            }
            
            return total1478;
        }

        public object B()
        {
            var data = File.ReadAllLines("Day8.txt");

            var sum = 0;
            foreach (var d in data)
            {
                var testValues = d.Replace(" |", "").Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var solution = Solve(new Dictionary<char, char>(), testValues);
                
                var result = d.Split('|')[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                int val = 0;
                foreach (var r in result)
                {
                    val = val * 10 + GetMappedNum(solution!, r)!.Value;
                }

                sum += val;
            }
            
            return sum;
        }
        
        private static Dictionary<char, char>? Solve(Dictionary<char, char> dict, ICollection<string> testVals)
        {
            var allChars = "abcdefg".ToArray();
            if (dict.Count == allChars.Length)
                return dict;
            
            var firstUnmapped = allChars.First(c => !dict.ContainsValue(c));
            foreach (var chr in allChars.Except(dict.Keys))
            {
                var d2 = new Dictionary<char, char>(dict);
                d2.Add(chr, firstUnmapped);

                if (!IsValid(d2, testVals)) continue;
                
                var solution = Solve(d2, testVals);
                if (solution != null)
                    return solution;
            }

            return null;
        }

        private static bool IsValid(Dictionary<char, char> dict, ICollection<string> testVals)
        {
            if (dict.Count != 7)
                return true;

            return testVals.All(val => GetMappedNum(dict, val) != null);
        }

        private static int? GetMappedNum(Dictionary<char, char> dict, string crypto)
        {
            var mapped = new string(crypto.Select(v => dict[v]).OrderBy(v => v).ToArray());
            if (AllNumbers.TryGetValue(mapped, out var val))
                return val;
            return null;
        }
    }
}