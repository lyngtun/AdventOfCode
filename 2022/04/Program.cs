using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var lines = System.IO.File.ReadLines("input.txt");

string pattern = @"(\d+)-(\d+),(\d+)-(\d+)";
Regex rg = new Regex(pattern);

var overlaps = 0;

bool isEnclosed((int, int) a, (int, int) b) {
    return (a.Item1 <= b.Item1 && a.Item2 >= b.Item2) || (b.Item1 <= a.Item1 && b.Item2 >= a.Item2);
}

bool isNoOverlap((int, int) a, (int, int) b) {
    return (a.Item1 < b.Item1 && a.Item2 < b.Item1) || (b.Item1 < a.Item1 && b.Item2 < a.Item1);
}


foreach(var line in lines) {
    var match = rg.Match(line);

    var ok = isNoOverlap((Int32.Parse(match.Groups[1].Value), Int32.Parse(match.Groups[2].Value)), 
        (Int32.Parse(match.Groups[3].Value), Int32.Parse(match.Groups[4].Value)));
    
    if(!ok) overlaps++;

    Console.WriteLine($"{line}, no overlap: {ok} overlaps: {overlaps}");
}