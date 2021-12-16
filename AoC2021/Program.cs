using AoC2021;


DateTime start = DateTime.Now;
string result = new Day15().B()?.ToString() ?? " ";
DateTime stop = DateTime.Now;

Console.WriteLine("It took " + (stop - start).TotalSeconds);

WindowsClipboard.SetText(result);
Console.WriteLine(result);

