var lines = System.IO.File.ReadLines("input/input.txt");


var points = new List<Point>();

foreach(var line in lines) {
    var point = line.Split(",");

    points.Add(new Point { point = (Int32.Parse(point[0]), Int32.Parse(point[1]), Int32.Parse(point[2])) });
}

for(var i=0; i<points.Count()-1; i++) {
    for(var j=i+1; j<points.Count(); j++) {
        if(isAjacent(points[i].point, points[j].point)) {
            points[i].coveredSides++;
            points[j].coveredSides++;
        }
    }
}

bool isAjacent((int x, int y, int z) a, (int x, int y, int z) b) {
    if(Math.Abs(a.x-b.x) > 1 || Math.Abs(a.y-b.y) > 1 || Math.Abs(a.z-b.z) > 1) {
        return false;
    }

    if(a.x == b.x) {
        if(a.y == b.y || a.z == b.z) {
            return true;
        }
    }
    else {
        return a.y == b.y && a.z == b.z;
    }

    return false;
}

var sumSides = 0;
foreach(var point in points) {
    sumSides += 6 - point.coveredSides;
}

Console.WriteLine($"Part 1 visible sides: {sumSides}");

public class Point {
    public (int x, int y, int z) point;
    public int coveredSides = 0;
}