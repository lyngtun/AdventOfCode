// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 3, part 1!");

var sum = 0;


int getValue(char a) {
    if(a > 'Z') {
        return ((int)a) - 96;
    }
    else {
        return ((int)a) - 38; 
    }
}


var lines = System.IO.File.ReadLines(@"input.txt");

var x = 0;
while(x < lines.Count()) {
    var first = lines.ElementAt(x++);
    var second = lines.ElementAt(x++);
    var third = lines.ElementAt(x++);

    char inAll = '0';
    foreach(var c in first.ToCharArray()) {
        if(second.IndexOf(c) != -1 && third.IndexOf(c) != -1) {
            inAll = c;
            break;
        }
    }

    if(inAll == '0') throw new Exception();

    sum += getValue(inAll);

    Console.WriteLine($"First: {first} Second: {second} Third: {third} In all: {inAll} Value: {getValue(inAll)}  Sum: {sum}");
}
