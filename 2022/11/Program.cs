var lines = System.IO.File.ReadAllLines("input/input.txt");


var monkeys = new List<Monkey>();
long lcd = 1;

for(int i=0; i<lines.Count(); i++) {
    if(!string.IsNullOrWhiteSpace(lines[i])) {

        // Monkey line
        var idString = lines[i++].Split(" ")[1];
        var id = Int32.Parse(idString.Remove(idString.Length -1));

        var items = lines[i++].Split(" ");
        var itemList = new List<long>();
        for(var j=4; j<items.Count(); j++) {
            if(items[j].Contains(',')) itemList.Add(Int32.Parse(items[j].Remove(items[j].Length-1)));
            else itemList.Add(Int32.Parse(items[j]));
        }

        var opStrings = lines[i++].Split(" ");
        var opType = opStrings[6];
        var opVal = opStrings[7];

        var testDivisor = Int32.Parse(lines[i++].Split(" ")[5]);
        var trueMonkey = Int32.Parse(lines[i++].Split(" ")[9]);
        var falseMonkey = Int32.Parse(lines[i++].Split(" ")[9]);

        monkeys.Add(new Monkey(id, itemList, opType, opVal, testDivisor, trueMonkey, falseMonkey));
    }
}

long doOperation(string type, string val, long item) {
    long opval = 0;
    
    if(val == "old") opval = item;
    else opval = Int32.Parse(val);

    switch(type) {
        case "+":
            return item + opval;
        case "*": 
            return item * opval;
    }

    throw new InvalidOperationException();
}

void doMonkeyBusiness(Monkey m) {
    foreach(var item in m.items) {
        m.inspectCount++;
        var worryLevel = doOperation(m.operationType, m.operationVal, item);
        worryLevel %= lcd;

        if(worryLevel % m.testDivisor == 0) monkeys[m.trueMonkey].items.Add(worryLevel);
        else monkeys[m.falseMonkey].items.Add(worryLevel);
    }
    m.items.Clear();
}

var numRounds = 10000;

foreach(var m in monkeys) {
    lcd *= m.testDivisor;
}

for(var i=0; i< numRounds; i++) {
    foreach(var monkey in monkeys) {
        doMonkeyBusiness(monkey);
    }
}

var maxInspect = new List<long>();

foreach(var m in monkeys) {
    Console.WriteLine($"Monkey {m.id} inspected {m.inspectCount} times.");
    maxInspect.Add(m.inspectCount);
}

maxInspect.Sort();
maxInspect.Reverse();
var part1 = maxInspect[0]*maxInspect[1];

Console.WriteLine($"Part 1: {part1}");


public class Monkey {
    public int id;
    public List<long> items;
    public string operationType;
    public string operationVal;
    public int testDivisor;
    public int trueMonkey;
    public int falseMonkey;
    public int inspectCount = 0;

    public Monkey(int id, List<long> items, string operationType, string operationVal, int testDivisor, int trueMonkey, int falseMonkey) {
        this.id = id;
        this.items = items;
        this.operationType = operationType;
        this.operationVal = operationVal;
        this.testDivisor = testDivisor;
        this.trueMonkey = trueMonkey;
        this.falseMonkey = falseMonkey;
    }
}
