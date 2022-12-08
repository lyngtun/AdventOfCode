// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var lines = System.IO.File.ReadLines(@"input.txt");

var treeMap = new List<List<int>>();
var currentLine = 0;
foreach(var line in lines) {
    treeMap.Add(new List<int>());
    foreach(var tree in line.ToCharArray()) {
        treeMap[currentLine].Add((int)Char.GetNumericValue(tree));
    }
    currentLine++;
}

bool isVisibleFromTop(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentY = 0; currentY < y; currentY++) {
        if(tree <= treeMap[currentY][x]) {
            return false;
        }
    }

    return true;
}

bool isVisibleFromBottom(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentY = treeMap.Count-1; currentY > y; currentY--) {
        if(tree <= treeMap[currentY][x]) {
            return false;
        }
    }

    return true;
}

bool isVisibleFromLeft(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentX = 0; currentX < x; currentX++) {
        if(tree <= treeMap[y][currentX]) {
            return false;
        }
    }

    return true;
}

bool isVisibleFromRight(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentX = treeMap[y].Count-1; currentX > x; currentX--) {
        if(tree <= treeMap[y][currentX]) {
            return false;
        }
    }

    return true;
}

bool isVisible(int x, int y) {
    if(x == 0 || x == treeMap[y].Count() - 1) return true;
    if(y == 0 || y == treeMap.Count() - 1) return true;

    return isVisibleFromTop(x, y) || isVisibleFromBottom(x, y) || isVisibleFromLeft(x, y) || isVisibleFromRight(x, y);
}

int visibleTrees = 0;

for(var x=0; x<treeMap[0].Count(); x++) {
    for(var y=0; y<treeMap.Count(); y++) {
        if(isVisible(x, y)) {
            visibleTrees++;
        }
    }
}

int maxScenicScore = 0;

int computeUp(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentY = y-1; currentY >= 0; currentY--) {
        if(tree <= treeMap[currentY][x]) {
            return y - currentY;
        }
    }

    return y;
}

int computeDown(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentY = y+1; currentY < treeMap.Count(); currentY++) {
        if(tree <= treeMap[currentY][x]) {
            return currentY - y;
        }
    }

    return treeMap.Count() - y - 1;
}

int computeLeft(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentX = x-1; currentX >= 0; currentX--) {
        if(tree <= treeMap[y][currentX]) {
            return x - currentX;
        }
    }

    return x;
}

int computeRight(int x, int y) {
    int tree = treeMap[y][x];

    for(var currentX = x+1; currentX < treeMap[0].Count(); currentX++) {
        if(tree <= treeMap[y][currentX]) {
            return currentX - x;
        }
    }

    return treeMap[0].Count() - x - 1;
}

void computeScenicScore(int x, int y) {
    if(x == 0 || x == treeMap[y].Count() - 1) return;
    if(y == 0 || y == treeMap.Count() - 1) return;

    var score = computeUp(x, y) * computeDown(x, y) * computeLeft(x, y) * computeRight(x, y);
    if(score > maxScenicScore) {
        maxScenicScore = score;
    }
}

for(var x=0; x<treeMap[0].Count(); x++) {
    for(var y=0; y<treeMap.Count(); y++) {
        computeScenicScore(x, y);
    }
}


Console.WriteLine($"Visible trees: {visibleTrees}");
Console.WriteLine($"Max scenic score: {maxScenicScore}");
