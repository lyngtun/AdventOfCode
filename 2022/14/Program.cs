using System.Text.RegularExpressions;

var lines = System.IO.File.ReadLines("input/input.txt");

var pattern = @"(\d+),(\d+)";
var rg = new Regex(pattern);
var paths = new List<Path>();

var xmax = 500;
var xmin = 500;
var ymax = 0;

foreach(var line in lines) {
    var path = new Path();
    var matches = rg.Matches(line);
    for(var i=0; i<matches.Count(); i++) {
        var x = Int32.Parse(matches[i].Groups[1].Value);
        var y = Int32.Parse(matches[i].Groups[2].Value);

        if(x > xmax) xmax = x;
        if(x < xmin) xmin = x;
        if(y > ymax) ymax = y;

        path.points.Add((x, y));
    }
    paths.Add(path);
}

// Make map
var myMap = new char[xmax+2, ymax+2];
for(var i=0; i<xmax+2; i++) {
    for(var j=0; j<ymax+2; j++) {
        myMap[i, j] = '.';
    }
}

void drawRocks((int x, int y) start, (int x, int y) finish) {
    if(start.x == finish.x) {
        int sy = start.y < finish.y ? start.y : finish.y;
        int fy = sy == start.y ? finish.y : start.y;

        for(int i=sy; i<=fy; i++) {
            myMap[start.x, i] = '#';
        }
    }
    else {
        int sx = start.x < finish.x ? start.x : finish.x;
        int fx = sx == start.x ? finish.x : start.x;

        for(int i=sx; i<=fx; i++) {
            myMap[i, start.y] = '#';
        }
    }
}

foreach(var path in paths) {
    for(var i=0; i<path.points.Count()-1; i++) {
        drawRocks(path.points[i], path.points[i+1]);
    }
}

// Pour sand
var stopPour = false;
var numStoppedSand = 0;
(int x, int y) sandPosition = (500, 0);

while(!stopPour) {
    if(myMap[sandPosition.x, sandPosition.y+1] == '.') {
        sandPosition.y++;
    }
    else if(myMap[sandPosition.x-1, sandPosition.y+1] == '.') {
        sandPosition.y++;
        sandPosition.x--;
    }
    else if(myMap[sandPosition.x+1, sandPosition.y+1] == '.') {
        sandPosition.y++;
        sandPosition.x++;
    }
    else {
        myMap[sandPosition.x, sandPosition.y] = 'o';
        numStoppedSand++;
        sandPosition = (500, 0);
    }

    if(sandPosition.y > ymax) {
        stopPour = true;
    }
}

Console.WriteLine($"Part 1 - number of settled sand: {numStoppedSand}");

public class Path {
    public List<(int, int)> points = new List<(int, int)>();
}