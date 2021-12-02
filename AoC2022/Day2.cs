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
                .Select(l => (direction: l[0], amount: int.Parse(l[1])))
                .GroupBy(key => key.direction, val => val.amount, (dir, val) => (dir, val: val.Sum()))
                .ToDictionary(key => key.dir, val => val.val);

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