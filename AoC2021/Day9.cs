namespace AoC2021
{
    public class Day9
    {
        public object A()
        {
            var heightMap = File.ReadLines("Day9.txt")
                .Select(l => l.Select(chr => chr - '0').ToArray()).ToArray();


            var score = 0;
            for (int y = 0; y < heightMap.Length; y++)
            for (int x = 0; x < heightMap[y].Length; x++)
            {
                var neighbours = GetNeighbours(x, y, heightMap).Select(n => heightMap[n.y][n.x]);
                if (heightMap[y][x] < neighbours.Min())
                    score += heightMap[y][x] + 1;
            }


            return score;
        }

        public object B()
        {
            var heightMap = File.ReadLines("Day9.txt")
                .Select(l => l.Select(chr => chr - '0').ToArray()).ToArray();

            int basinNum = -1;
            for (int y = 0; y < heightMap.Length; y++)
            for (int x = 0; x < heightMap[y].Length; x++)
            {
                var neighbours = GetNeighbours(x, y, heightMap).Select(n => heightMap[n.y][n.x]);
                if (heightMap[y][x] < neighbours.Min())
                    SetBasin(x, y, heightMap, basinNum--);
            }

            var basinSizes =
                from h in heightMap.SelectMany(h => h)
                where h != 9
                group h by h
                into grp
                select grp.Count();

            return basinSizes.OrderByDescending(s => s).Take(3).Aggregate((a, b) => a * b);
        }

        private static void SetBasin(int x, int y, int[][] map, int numToSet)
        {
            map[y][x] = numToSet;
            foreach (var n in GetNeighbours(x, y, map))
            {
                if (map[n.y][n.x] > 0 && map[n.y][n.x] != 9)
                    SetBasin(n.x, n.y, map, numToSet);
            }
        }


        private static IEnumerable<(int x, int y)> GetNeighbours(int x, int y, int[][] map)
        {
            if (x > 0)
                yield return (x - 1, y);
            if (y > 0)
                yield return (x, y - 1);
            if (x + 1 < map[0].Length)
                yield return (x + 1, y);
            if (y + 1 < map.Length)
                yield return (x, y + 1);
        }
    }
}