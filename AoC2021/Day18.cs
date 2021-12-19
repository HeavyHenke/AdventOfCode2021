using System.Text.RegularExpressions;

namespace AoC2021;

public class Day18
{
    public object A()
    {
        var lines = File.ReadAllLines("Day18.txt");
        var num = lines[0];

        for (int i = 1; i < lines.Length; i++)
        {
            num = Add(num, lines[i]);
            num = Reduce(num);
        }
        
        return SnailFishNum.Parse(num).CalcMagnitude();
    }

    public object B()
    {
        var lines = File.ReadAllLines("Day18.txt");

        var largest = int.MinValue;
        for(int num1 = 0; num1 < lines.Length; num1++)
        for (int num2 = 0; num2 < lines.Length; num2++)
        {
            if(num1 == num2)
                continue;
            var reducedSum = Reduce(Add(lines[num1], lines[num2]));
            var mag = SnailFishNum.Parse(reducedSum).CalcMagnitude();
            if (mag > largest)
                largest = mag;
        }

        return largest;
    }

    private static string Reduce(string num)
    {
        while (true)
        {
            var bang = Explode(num);
            if (bang != null)
            {
                num = bang;
                continue;
            }

            var split = Split(num);
            if (split != null)
            {
                num = split;
                continue;
            }

            return num;
        }
    }

    private static string? Split(string num)
    {
        var m = Regex.Match(num, @"\d{2,}");
        if (m.Success == false)
            return null;

        var splitNum = int.Parse(m.Groups[0].Value);
        var s1 = splitNum / 2;
        var s2 = (splitNum + 1) / 2;

        var ret = num[..m.Index] + $"[{s1},{s2}]" + num[(m.Index + m.Length)..];
        return ret;
    }
    
    private static string Add(string num1, string num2)
    {
        return $"[{num1},{num2}]";
    }
    
    private static string? Explode(string num)
    {
        int nesting = 0;
        for (int pos = 0; pos < num.Length; pos++)
        {
            if (num[pos] == '[')
            {
                nesting++;
            }
            else if (num[pos] == ']')
            {
                nesting--;
            }

            if (nesting == 5)
            {
                var groupMatch = Regex.Match(num[pos..], @"(\d+),(\d+)(\])");
                var leftNum = int.Parse(groupMatch.Groups[1].Value);
                var rightNum = int.Parse(groupMatch.Groups[2].Value);
                var end = groupMatch.Groups[3].Index;
                var explodedNum = num[..pos] + '0' + num[(pos + end + 1)..];

                var m2 = Regex.Matches(explodedNum, @"\d+");
                var rightOfExplosion = m2.FirstOrDefault(m => m.Index > pos);
                if (rightOfExplosion != null)
                {
                    var replacementNum = int.Parse(rightOfExplosion.Value) + rightNum;
                    explodedNum = explodedNum[..rightOfExplosion.Index] + replacementNum + explodedNum[(rightOfExplosion.Index + rightOfExplosion.Length)..];
                }

                var leftOfExplosion = m2.Reverse().FirstOrDefault(m => m.Index < pos - 1);
                if (leftOfExplosion != null)
                {
                    var replacementNum = int.Parse(leftOfExplosion.Value) + leftNum;
                    explodedNum = explodedNum[..leftOfExplosion.Index] + replacementNum + explodedNum[(leftOfExplosion.Index + leftOfExplosion.Length)..];
                }
                
                return explodedNum;
            }
        }

        return null;
    }


    class SnailFishNum
    {
        private int? _regularNumber;
        private SnailFishNum? _left;
        private SnailFishNum? _right;

        public int CalcMagnitude()
        {
            if (_regularNumber.HasValue)
                return _regularNumber.Value;

            return 3 * _left!.CalcMagnitude() + 2 * _right!.CalcMagnitude();
        }


        public static SnailFishNum Parse(string str)
        {
            int pos = 0;
            return Parse(str, ref pos);
        }
        private static SnailFishNum Parse(string str, ref int pos)
        {
            var result = new SnailFishNum();
            if (str[pos] != '[')
                throw new Exception("Förväntade mig en [");
            if (str[pos + 1] == '[')
            {
                pos++;
                result._left = Parse(str, ref pos);
            }
            else if (char.IsDigit(str[pos + 1]))
            {
                result._left = new SnailFishNum { _regularNumber = str[pos + 1] - '0' };
                pos += 2;
            }

            if (str[pos] != ',')
                throw new Exception("Förväntade mig en ,");

            if (str[pos + 1] == '[')
            {
                pos++;
                result._right = Parse(str, ref pos);
            }
            else if (char.IsDigit(str[pos + 1]))
            {
                result._right = new SnailFishNum { _regularNumber = str[pos + 1] - '0' };
                pos += 2;
            }

            pos++;
            return result;
        }
    }
}