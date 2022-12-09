
var lines = System.IO.File.ReadLines(@"input.txt");

var hpos = (0, 0);
var tpos = (0, 0);

var allTailPositions = new List<(int, int)>();
allTailPositions.Add(tpos);

void moveHead(string direction, int count) {
    for(int i=0; i < count; i++) {
        switch(direction) {
            case "U": 
                hpos.Item2 += 1;
                break;
            case "D":
                hpos.Item2 -= 1;
                break;
            case "R":
                hpos.Item1 += 1;
                break;
            case "L":
                hpos.Item1 -= 1;
                break;
        }
    
        moveTail();
    }
}

void moveTail() {
    if(hpos.Item1 - tpos.Item1 > 1) {
        tpos.Item1 += 1;
        tpos.Item2 = hpos.Item2;
    }
    else if(tpos.Item1 - hpos.Item1 > 1) {
        tpos.Item1 -= 1;
        tpos.Item2 = hpos.Item2;
    }
    else if(hpos.Item2 - tpos.Item2 > 1) {
        tpos.Item2 += 1;
        tpos.Item1 = hpos.Item1;
    }
    else if(tpos.Item2 - hpos.Item2 > 1) {
        tpos.Item2 -= 1;
        tpos.Item1 = hpos.Item1;
    }

   if(!allTailPositions.Contains(tpos)) {
        allTailPositions.Add(tpos);
    }
}

foreach(var line in lines) {
    var cmd = line.Split(" ");

    moveHead(cmd[0], Int32.Parse(cmd[1]));
}

Console.WriteLine($"Total tail positions: {allTailPositions.Count()}");