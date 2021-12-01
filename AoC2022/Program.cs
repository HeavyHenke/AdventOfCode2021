using System;
using AoC2022;


Console.WriteLine("Hello, World!");

DateTime start = DateTime.Now;
string result = new Day1().B()?.ToString() ?? " ";
DateTime stop = DateTime.Now;

Console.WriteLine("It took " + (stop - start).TotalSeconds);

WindowsClipboard.SetText(result);
Console.WriteLine(result);
