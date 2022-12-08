// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, 06!");

var line = System.IO.File.ReadLines("input.txt").First();

int pos = 0;
string start = "";
for(pos = 0; pos < line.Length; pos++) {
    if(start.Contains(line[pos])) {
        start = start.Substring(start.IndexOf(line[pos])+1);
    }

    start += line[pos];
    if(start.Length == 14) {
        break;
    }
}

Console.WriteLine($"Found start '{start}', last pos: {pos}.");
