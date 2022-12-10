var lines = System.IO.File.ReadLines(@"input/input.txt").ToArray();

var currentCycle = 1;
var checkCycle = 20;
const int checkInterval = 40;
var checkVals = new List<int>();
var regX = 1;

List<char> display = new List<char>();

void computeCycle(int cycle, int spritePos) {
    var colNo = (cycle-1) % 40;
    if(colNo >= spritePos-1 && colNo <= spritePos+1) {
        display.Add('#');
    }
    else {
        display.Add('.');
    }
}

foreach(var line in lines) {
    var cmd = line.Split(" ");
    if(cmd[0] == "noop") {
        if(currentCycle == checkCycle) {
            checkVals.Add(regX * checkCycle);
            Console.WriteLine($"Cycle {currentCycle}, value {regX}, add val {regX*checkCycle}");
            checkCycle += checkInterval;
        }

        computeCycle(currentCycle, regX);
        currentCycle++;
    }
    else if(cmd[0] == "addx") {
        var val = Int32.Parse(cmd[1]);

        if(currentCycle == checkCycle || currentCycle+1 == checkCycle) {
            checkVals.Add(regX * checkCycle);
            Console.WriteLine($"Cycle {currentCycle}, value {regX}, add val {regX*checkCycle}");
            checkCycle += checkInterval;
        }

        computeCycle(currentCycle, regX);
        computeCycle(currentCycle+1, regX);
        currentCycle += 2;
        regX += val;
    }
}

var sum = checkVals.Sum();
Console.WriteLine($"Sum of check cycle products: {sum}");

var line1 = new string(display.GetRange(0, 40).ToArray());
var line2 = new string(display.GetRange(40, 40).ToArray());
var line3 = new string(display.GetRange(80, 40).ToArray());
var line4 = new string(display.GetRange(120, 40).ToArray());
var line5 = new string(display.GetRange(160, 40).ToArray());
var line6 = new string(display.GetRange(200, 40).ToArray());

Console.WriteLine(line1);
Console.WriteLine(line2);
Console.WriteLine(line3);
Console.WriteLine(line4);
Console.WriteLine(line5);
Console.WriteLine(line6);
