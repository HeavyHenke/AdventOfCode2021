using System.Text;

namespace AoC2021;

public class Day14
{
    public object A()
    {
        var lines = File.ReadAllLines("Day14.txt");
        var str = lines[0];

        Dictionary<string, char> rules = new();
        for (int i = 2; i < lines.Length; i++)
        {
            var pair = lines[i].Split(" -> ");
            rules.Add(pair[0], pair[1][0]);
        }

        for (int step = 0; step < 10; step++)
        {
            var outStr = new StringBuilder();
            for (int i = 0; i < str.Length - 1; i++)
            {
                outStr.Append(str[i]);
                if (rules.TryGetValue(str.Substring(i, 2), out var addChar))
                    outStr.Append(addChar);
            }

            outStr.Append(str[^1]);
            str = outStr.ToString();
        }

        var counts = str.GroupBy(c => c, (_, chars) => chars.Count())
            .OrderBy(c => c)
            .ToList();
        return counts.Last() - counts.First();
    }

    public object B()
    {
        var lines = File.ReadAllLines("Day14.txt");

        Dictionary<string, char> rules = new();
        for (int i = 2; i < lines.Length; i++)
        {
            var pair = lines[i].Split(" -> ");
            rules.Add(pair[0], pair[1][0]);
        }

        var strCounts = new Dictionary<string, long>();
        for (int i = 0; i < lines[0].Length - 1; i++)
        {
            var part = lines[0].Substring(i, 2);
            AddCountToDict(strCounts, part, 1);
        }

        for (int step = 0; step < 40; step++)
        {
            var strCountsOut = new Dictionary<string, long>();
            foreach (var kvp in strCounts)
            {
                var add = rules[kvp.Key];
                AddCountToDict(strCountsOut, new String(new[] { kvp.Key[0], add }), kvp.Value);
                AddCountToDict(strCountsOut, new String(new[] { add, kvp.Key[1] }), kvp.Value);
            }

            strCounts = strCountsOut;
        }

        var totalDict = new Dictionary<string, long>();
        foreach (var (key, value) in strCounts)
        {
            AddCountToDict(totalDict, key[0].ToString(), value);
            AddCountToDict(totalDict, key[1].ToString(), value);
        }
        AddCountToDict(totalDict, lines[0][0].ToString(), 1);   // Add first and last letter so that they also counts twice
        AddCountToDict(totalDict, lines[0][^1].ToString(), 1);
        
        return (totalDict.Values.Max() - totalDict.Values.Min())/2;
    }

    private static void AddCountToDict(IDictionary<string, long> dict, string str, long count)
    {
        if (dict.TryGetValue(str, out var dictCount))
            dict[str] = dictCount + count;
        else
            dict.Add(str, count);
    }
}