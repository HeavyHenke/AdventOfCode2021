using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq.Extensions;

namespace AoC2022
{
    class Day4
    {
        private class Board
        {
            private readonly List<HashSet<int>> _allLinesAndCols;

            public Board(IEnumerable<string> linesForBoard)
            {
                var data = linesForBoard.Skip(1).Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();

                // Rows and cols
                _allLinesAndCols = data.Concat(data.Transpose()).Select(d => new HashSet<int>(d)).ToList();
            }

            public int GetWinScore(int drawnNum, HashSet<int> allNums)
            {
                if (_allLinesAndCols.All(x => x.Except(allNums).Any()))
                    return -1;

                return _allLinesAndCols.SelectMany(x => x).Except(allNums).Distinct().Sum() * drawnNum;
            }
        }

        public object A()
        {
            var lines = File.ReadAllLines("Day4.txt");
            var drawOrder = lines[0].Split(',').Select(int.Parse).ToList();
            var boards = lines.Skip(1).Batch(6).Select(linesForBoard => new Board(linesForBoard)).ToList();

            var drawnNumbers = new HashSet<int>();
            foreach (var num in drawOrder)
            {
                drawnNumbers.Add(num);
                foreach (var b in boards)
                {
                    var score = b.GetWinScore(num, drawnNumbers);
                    if (score != -1)
                        return score;
                }
            }

            return 0;
        }

        public object B()
        {
            var lines = File.ReadAllLines("Day4.txt");
            var drawOrder = lines[0].Split(',').Select(int.Parse).ToList();
            var boards = lines.Skip(1).Batch(6).Select(linesForBoard => new Board(linesForBoard)).ToList();

            var drawnNumbers = new HashSet<int>();
            foreach (var num in drawOrder)
            {
                drawnNumbers.Add(num);
                foreach (var b in boards.ToList())
                {
                    var score = b.GetWinScore(num, drawnNumbers);
                    if (score != -1)
                    {
                        if (boards.Count == 1)
                            return score;
                        boards.Remove(b);
                    }
                }
            }

            return 0;
        }
    }
}