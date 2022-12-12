var lines = System.IO.File.ReadAllLines("input/input.txt");

var xsize = lines[0].Length;
var ysize = lines.Count();

var myMap = new char[xsize, ysize];
var pathMap = new char[xsize, ysize];

(int, int) startPos = (-1, -1);
(int, int) endPos = (-1, -1);

for(var i=0; i<ysize; i++) {
    for(var j=0; j<xsize; j++) {
        pathMap[j, i] = '.';
        myMap[j, i] = lines[i][j];
        if(myMap[j, i] == 'S') {
            startPos = (j, i);
        }
        else if(myMap[j, i] == 'E') {
            endPos = (j, i);
        }
    }
}



Console.WriteLine($"Start pos: {startPos}");
Console.WriteLine($"End pos: {endPos}");

List<(int, int, int)> getAjacent((int , int , int) location) {
    var positions = new List<(int, int, int)> { 
        (location.Item1-1, location.Item2, location.Item3+1), 
        (location.Item1+1, location.Item2, location.Item3+1), 
        (location.Item1, location.Item2+1, location.Item3+1), 
        (location.Item1, location.Item2-1, location.Item3+1), 
    };

    return positions;
}

List<(int, int, int)> eliminateVisited(List<(int, int, int)> newLocations, List<(int, int, int)> path) {
    var notVisited = new List<(int, int, int)>();

    foreach(var pos in newLocations) {
        var isValid = true;
        foreach(var pathPos in path) {
            if(pos.Item1 == pathPos.Item1 && pos.Item2 == pathPos.Item2) {
                isValid = false;
                break;
            }
            if(pos.Item1 < 0 || pos.Item1 >= xsize || pos.Item2 >= ysize || pos.Item2 < 0) {
                isValid = false;
                break;
            }
        }

        if(isValid) {
            notVisited.Add(pos);
        }
    }

    return notVisited;
}

List<(int, int, int)> eliminateHeightDifference(List<(int, int, int)> newLocations, (int, int, int) pos) {
    var okHeight = new List<(int, int, int)>();
    var height = myMap[pos.Item1, pos.Item2];

    if(height == 'S') height = 'a';
    if(height == 'E') height = 'z';
    foreach(var newPos in newLocations) {
        var newHeight = myMap[newPos.Item1, newPos.Item2];
        if(newHeight == 'S') newHeight = 'a';
        if(newHeight == 'E') newHeight = 'z';
        if((int)height - (int)newHeight < 2) {
            okHeight.Add(newPos);
        }
    }

    return okHeight;
}

(int, int, int) findStart(List<(int, int, int)> newLocations) {
    foreach(var newPos in newLocations) {
        if(myMap[newPos.Item1, newPos.Item2] == 'S') return newPos;
    }

    return (-1, -1, -1);
}


void printPathMap() {
    Console.WriteLine();
    for(var y=0;y<ysize;y++) {
        for(var x=0;x<xsize;x++) {
            Console.Write(pathMap[x, y]);
        }
        Console.WriteLine();
    }
}

// Create list with endpoint, distance 0
var path = new List<(int, int, int)>();
path.Add((endPos.Item1, endPos.Item2, 0));

for(var i=0; i<path.Count(); i++) {
    var currentLocation = path[i];
    // pathMap[currentLocation.Item1, currentLocation.Item2] = myMap[currentLocation.Item1, currentLocation.Item2];
    // if(i % 10 == 0) printPathMap();

    var newLocations = getAjacent(currentLocation);

    // Eliminate visited
    newLocations = eliminateVisited(newLocations, path);
    
    // Eliminate height
    newLocations = eliminateHeightDifference(newLocations, currentLocation);

    // Check for goal
    var start = findStart(newLocations);
    if(start != (-1, -1, -1)) {
        // Found start!
        Console.WriteLine($"Start found at: {start}");
    }

    path.AddRange(newLocations);
}


Console.WriteLine("Search complete");