using System;
using System.IO;
using System.Linq;

namespace AoC2021
{
    public class Day7
    {
        public object A()
        {
            //var data = "16,1,2,0,4,2,7,1,2,14".Split(',').Select(int.Parse).ToArray();
            var data = File.ReadAllLines("Day7.txt")[0].Split(',').Select(int.Parse).ToArray();

            var bestFuelCost = int.MaxValue;
            for (int i = data.Min(); i < data.Max(); i++)
            {
                var fuelCost = data.Sum(d => Math.Abs(d-i));
                if(fuelCost > bestFuelCost)
                    break;
                if (fuelCost < bestFuelCost)
                    bestFuelCost = fuelCost;
            }
            
            return bestFuelCost;
        }
        
        public object B()
        {
            //var data = "16,1,2,0,4,2,7,1,2,14".Split(',').Select(int.Parse).ToArray();
            var data = File.ReadAllLines("Day7.txt")[0].Split(',').Select(int.Parse).ToArray();

            var bestFuelCost = int.MaxValue;
            for (int i = data.Min(); i < data.Max(); i++)
            {
                var fuelCost = data.Sum(d => FuelCost(Math.Abs(d-i)));
                if(fuelCost > bestFuelCost)
                    break;
                if (fuelCost < bestFuelCost)
                    bestFuelCost = fuelCost;
            }
            
            
            return bestFuelCost;
        }

        private static int FuelCost(int dist)
        {
            var cost = 0;
            while (dist > 0)
            {
                cost += dist;
                dist--;
            }

            return cost;
        }
    }
}