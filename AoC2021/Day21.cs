namespace AoC2021;

public class Day21
{
    private List<(int dieSum, int numTimes)> _dieTable;
    private readonly Dictionary<(int pos1, int pos2, int score1, int score2), (long p1Wins, long p2Wins)> _memory = new();

    public object A()
    {
        int dice = 1;
        int numRolls = 0;

        int p1 = 3;
        long p1Score = 0;
        int p2 = 7;
        long p2Score = 0;

        int RollDice()
        {
            var val = dice;
            dice++;
            if (dice > 100)
                dice = 1;
            numRolls++; 
            return val;
        }

        while (true)
        {
            var p1Roll = RollDice() + RollDice() + RollDice();
            p1 += p1Roll;
            p1 = (p1 - 1) % 10 + 1;
            p1Score += p1;
            if (p1Score >= 1000)
                return p2Score * numRolls;

            var p2Roll = RollDice() + RollDice() + RollDice();
            p2 += p2Roll;
            p2 = (p2 - 1) % 10 + 1;
            p2Score += p2;

            if (p2Score >= 1000)
                return p1Score * numRolls;
        }
    }


    private static IEnumerable<(int dieSum, int numTimes)> CreateDieTable()
    {
        var data = new List<int>();
        for(int die1 = 1; die1 <= 3; die1++)
        for(int die2=1;die2 <=3;die2++)
        for (int die3 = 1; die3 <= 3; die3++)
        {
            data.Add(die1 + die2 + die3);
        }

        var q = from d in data
            group d by d
            into g
            select (g.Key, g.Count());
        return q;
    }
    
    public object B()
    {
        _dieTable = CreateDieTable().ToList();

        var result = Solve(3, 7, 0, 0);

        Console.WriteLine("R: " + result);
        
        return Math.Max(result.p1Wins, result.p2Wins);
    }

    private (long p1Wins, long p2Wins) Solve(int pos1, int pos2, int score1, int score2)
    {
        if (_memory.TryGetValue((pos1, pos2, score1, score2), out var memoryResult))
            return memoryResult;

        long p1Wins = 0;
        long p2Wins = 0;

        foreach (var p1 in _dieTable)
        {
            var p1Pos = pos1 + p1.dieSum;
            p1Pos = (p1Pos - 1) % 10 + 1;
            var p1Score = score1 + p1Pos;
            if (p1Score >= 21)
            {
                p1Wins += p1.numTimes;
                continue;
            }
            
            foreach (var p2 in _dieTable)
            {
                long numTimes = p1.numTimes * p2.numTimes;

                var p2Pos = pos2 + p2.dieSum;
                p2Pos = (p2Pos - 1) % 10 + 1;
                var p2Score = score2 + p2Pos;
                if (p2Score >= 21)
                {
                    p2Wins += numTimes;
                    continue;
                }

                var result = Solve(p1Pos, p2Pos, p1Score, p2Score);
                p1Wins += result.p1Wins * numTimes;
                p2Wins += result.p2Wins * numTimes;
            }
        }

        _memory.Add((pos1, pos2, score1, score2), (p1Wins, p2Wins));
        return (p1Wins, p2Wins);
    }
}