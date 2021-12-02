using System.IO;
using System.Linq;

namespace AoC2022
{
    public class Day2
    {
        public object A()
        {
            var data = File.ReadAllLines("Day2.txt")
                .Select(l => l.Split(' '))
                .GroupBy(key => key[0], val => int.Parse(val[1]), (dir, val) => (dir, amount: val.Sum()))
                .ToDictionary(key => key.dir, val => val.amount);

            return (data["down"] - data["up"]) * data["forward"];
        }
        
        public object B()
        {
            var data = File.ReadAllLines("Day2.txt")
                .Select(l => l.Split(' '))
                .Select(l => (direction: l[0], amount: int.Parse(l[1])));

            int aim = 0;
            int depth = 0;
            int horizontalPos = 0;
            foreach (var (direction, amount) in data)
            {
                switch (direction)
                {
                    case "down":
                        aim += amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                    case "forward":
                        horizontalPos += amount;
                        depth += aim * amount;
                        break;
                }

            }

            return horizontalPos * depth;
        }
    }
}