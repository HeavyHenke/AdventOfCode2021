namespace AoC2021;

public class Day10
{
    public object A()
    {
        var lines = File.ReadLines("Day10.txt");

        var pointsByChar = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        var totalPoints = 0;
        foreach (var line in lines)
        {
            var minimizedLine = MinimizeLine(line);

            var firstError = minimizedLine.FirstOrDefault(c => pointsByChar.ContainsKey(c));
            if (firstError != default)
                totalPoints += pointsByChar[firstError];
        }
        
        return totalPoints;
    }

    private static string MinimizeLine(string line)
    {
        int lengthBefore;
        int lengthAfter;
        do
        {
            lengthBefore = line.Length;
            
            line = line
                .Replace("()", "")
                .Replace("[]", "")
                .Replace("{}", "")
                .Replace("<>", "");
            
            lengthAfter = line.Length;
        } while (lengthAfter != lengthBefore);

        return line;
    }

    public object B()
    {
        var lines = File.ReadLines("Day10.txt");

        var pointByChar = new Dictionary<char, int>
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };
        var closingChars = new HashSet<char>(")]}>");

        var pointList = new List<long>();
        foreach (var line in lines)
        {
            string minimizedLine = MinimizeLine(line);
            var firstError = minimizedLine.FirstOrDefault(c => closingChars.Contains(c));
            if (firstError == default)
            {
                long point = 0;
                foreach (var chr in minimizedLine.Reverse())
                    point = point * 5 + pointByChar[chr];
                pointList.Add(point);
            }
        }

        pointList.Sort();
        return pointList[pointList.Count / 2  ];
    }
}