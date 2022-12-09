
var lines = System.IO.File.ReadLines(@"input.txt");

var elements = 10;

List<(int, int)> positions = new List<(int, int)>();
for(int i=0; i < elements; i++) {
    positions.Add((0, 0));
}

var allTailPositions = new List<(int, int)>();
allTailPositions.Add(positions.Last());

void moveHead(string direction, int count) {

    for(int i=0; i < count; i++) {
        var hpos = positions.First();
    
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
        
        positions.RemoveAt(0);
        positions.Insert(0, hpos);
        moveTail(elements);
    }
}

void moveTail(int elements) {
    for(int i = 1; i < elements; i++) {
        var hpos = positions[i-1];
        var tpos = positions[i];

        if(!(Math.Abs(hpos.Item1 - tpos.Item1) < 2 && Math.Abs(hpos.Item2 - tpos.Item2) < 2)) {

            if(hpos.Item1 > tpos.Item1) tpos.Item1++;
            if(hpos.Item1 < tpos.Item1) tpos.Item1--;
            if(hpos.Item2 > tpos.Item2) tpos.Item2++;
            if(hpos.Item2 < tpos.Item2) tpos.Item2--;

            positions.RemoveAt(i);
            positions.Insert(i, tpos);
        }
    }

    var tail = positions.Last();

    if(!allTailPositions.Contains(tail)) {
          allTailPositions.Add(tail);
    }
}

foreach(var line in lines) {
    var cmd = line.Split(" ");

    moveHead(cmd[0], Int32.Parse(cmd[1]));
}

Console.WriteLine($"Total tail positions: {allTailPositions.Count()}");