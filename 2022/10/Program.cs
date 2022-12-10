var lines = System.IO.File.ReadLines(@"input/input.txt");

var currentCycle = 1;
var checkCycle = 20;
const int checkInterval = 40;
var checkVals = new List<int>();
var regX = 1;

foreach(var line in lines) {
    var cmd = line.Split(" ");
    if(cmd[0] == "noop") {
        if(currentCycle == checkCycle) {
            checkVals.Add(regX * checkCycle);
            Console.WriteLine($"Cycle {currentCycle}, value {regX}, add val {regX*checkCycle}");
            checkCycle += checkInterval;
        }
        currentCycle++;
    }
    else if(cmd[0] == "addx") {
        var val = Int32.Parse(cmd[1]);

        if(currentCycle == checkCycle || currentCycle+1 == checkCycle) {
            checkVals.Add(regX * checkCycle);
            Console.WriteLine($"Cycle {currentCycle}, value {regX}, add val {regX*checkCycle}");
            checkCycle += checkInterval;
        }
        currentCycle += 2;
        regX += val;
    }
}

var sum = checkVals.Sum();
Console.WriteLine($"Sum of check cycle products: {sum}");