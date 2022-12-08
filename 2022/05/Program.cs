using System.Text.RegularExpressions;


var line = System.IO.File.ReadLines("input.txt").ToList();

var stacks = new List<List<char>> { new List<char>(), new List<char>(), new List<char>(), new List<char>(), new List<char>(), new List<char>(), new List<char>(), new List<char>(), new List<char>()};


void decryptLine(string line) {
    for(int i=0;i<9;i++) {
        if(line.Length < 1+i*4) 
        {
            break;
        }

        var crate = line[1+(i*4)];
        if(!char.IsWhiteSpace(crate)) {
            stacks[i].Add(crate);
        }
    }
}

void doMove(int amount, int from, int to) {
    for(int i=0;i<amount;i++) {
        var crate = stacks[from-1][0];
        stacks[to-1].Insert(0, crate);
        stacks[from-1].RemoveAt(0);
    }
}

void doMove2(int amount, int from, int to) {

    var crates = stacks[from-1].GetRange(0, amount);
    stacks[to-1].InsertRange(0, crates);
    stacks[from-1].RemoveRange(0, amount);
}


string line = lines[0];


while(line.Contains('[')) 
{
    lines.RemoveAt(0);
    decryptLine(line);
    line = lines[0];
}

lines.RemoveAt(0);
lines.RemoveAt(0);

string pattern = @"move (\d+) from (\d+) to (\d+)";
Regex rg = new Regex(pattern);

foreach(var move in lines) {
    var match = rg.Match(move);

    doMove2(Int32.Parse(match.Groups[1].Value), Int32.Parse(match.Groups[2].Value), 
        Int32.Parse(match.Groups[3].Value));
}
 

foreach(var crate in stacks) {
    Console.Write(crate[0]);
}

Console.WriteLine("");
Console.WriteLine("Yup");
