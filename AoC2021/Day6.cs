using System.Linq;

namespace AoC2021
{
    public class Day6
    {
        public object A()
        {
            var input = Input
                .Split(',')
                .Select(int.Parse);
            
            var spawnTimers = new int[9];
            foreach (var age in input)
                spawnTimers[age]++;

            for (int day = 0; day < 80; day++)
            {
                int spawning = spawnTimers[0];
                for (int i = 1; i <= 8; i++)
                    spawnTimers[i - 1] = spawnTimers[i];
                
                spawnTimers[6] += spawning;
                spawnTimers[8] = spawning;
            }
            
            return spawnTimers.Sum();
        }

        public object B()
        {
            var input = Input
                .Split(',')
                .Select(int.Parse);
            
            var spawnTimers = new long[9];
            foreach (var age in input)
                spawnTimers[age]++;

            for (int day = 0; day < 256; day++)
            {
                long spawning = spawnTimers[0];
                for (int i = 1; i <= 8; i++)
                    spawnTimers[i - 1] = spawnTimers[i];
                
                spawnTimers[6] += spawning;
                spawnTimers[8] = spawning;
            }
            
            return spawnTimers.Sum();
        }
        
        private const string TestInput = "3,4,3,1,2";
        private const string Input = "2,5,5,3,2,2,5,1,4,5,2,1,5,5,1,2,3,3,4,1,4,1,4,4,2,1,5,5,3,5,4,3,4,1,5,4,1,5,5,5,4,3,1,2,1,5,1,4,4,1,4,1,3,1,1,1,3,1,1,2,1,3,1,1,1,2,3,5,5,3,2,3,3,2,2,1,3,1,3,1,5,5,1,2,3,2,1,1,2,1,2,1,2,2,1,3,5,4,3,3,2,2,3,1,4,2,2,1,3,4,5,4,2,5,4,1,2,1,3,5,3,3,5,4,1,1,5,2,4,4,1,2,2,5,5,3,1,2,4,3,3,1,4,2,5,1,5,1,2,1,1,1,1,3,5,5,1,5,5,1,2,2,1,2,1,2,1,2,1,4,5,1,2,4,3,3,3,1,5,3,2,2,1,4,2,4,2,3,2,5,1,5,1,1,1,3,1,1,3,5,4,2,5,3,2,2,1,4,5,1,3,2,5,1,2,1,4,1,5,5,1,2,2,1,2,4,5,3,3,1,4,4,3,1,4,2,4,4,3,4,1,4,5,3,1,4,2,2,3,4,4,4,1,4,3,1,3,4,5,1,5,4,4,4,5,5,5,2,1,3,4,3,2,5,3,1,3,2,2,3,1,4,5,3,5,5,3,2,3,1,2,5,2,1,3,1,1,1,5,1";
    }
}