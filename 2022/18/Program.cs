var lines = System.IO.File.ReadLines("input/input.txt");

var points = new List<Point>();
var dp = new List<(int, int, int)>();

var minx = Int32.MaxValue;
var miny = Int32.MaxValue;
var minz = Int32.MaxValue;
var maxx = Int32.MinValue;
var maxy = Int32.MinValue;
var maxz = Int32.MinValue;


foreach(var line in lines) {
    var point = line.Split(",");

    var x = Int32.Parse(point[0]);
    var y = Int32.Parse(point[1]);
    var z = Int32.Parse(point[2]);

    minx = x < minx ? x : minx;
    miny = y < miny ? y : miny;
    minz = z < minz ? z : minz;
    maxx = x > maxx ? x : maxx;
    maxy = y > maxy ? y : maxy;
    maxz = z > maxz ? z : maxz;

    dp.Add((x, y, z));
    points.Add(new Point { point = (x, y, z) });
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


var bbmin = (minx-1, miny-1, minz-1);
var bbmax = (maxx+1, maxy+1, maxz+1);

var space = new Space(maxx, maxy, maxz, minx, miny, minz);
var surfaceCount = 0;

void traverseSpace((int x, int y, int z) min, (int x, int y, int z) max) {
    var searchQueue = new List<(int x, int y, int z)>();

    searchQueue.Add(min);
    while(searchQueue.Count() > 0) {
        var current = searchQueue.First();
        searchQueue.RemoveAt(0);

        if(!space.hasVisited((current.x, current.y, current.z))) {
            // Not been here!
            space.visit((current.x, current.y, current.z));

            var ajacentPoints = new List<(int, int, int)>();
        
            // Add all ajacent sides to search queue
            if(current.x < maxx+1) ajacentPoints.Add((current.x + 1, current.y, current.z));
            if(current.y < maxy+1) ajacentPoints.Add((current.x, current.y + 1, current.z));
            if(current.z < maxz+1) ajacentPoints.Add((current.x, current.y, current.z + 1));
            if(current.x > minx-1) ajacentPoints.Add((current.x - 1, current.y, current.z));
            if(current.y > miny-1) ajacentPoints.Add((current.x, current.y - 1, current.z));
            if(current.z > minz-1) ajacentPoints.Add((current.x, current.y, current.z - 1));
            
            // Count surfaces for ajacent points
            for(int i=0; i<ajacentPoints.Count(); i++) {
                if(dp.Contains(ajacentPoints[i])) {
                    surfaceCount++;
                    ajacentPoints.RemoveAt(i);
                    i--;
                }
            }

            searchQueue.AddRange(ajacentPoints);
        }
    }
}

traverseSpace(bbmin, bbmax);

Console.WriteLine($"Part 2 surface points: {surfaceCount}");

public class Space {
    bool[,,] space;

    int dx;
    int dy;
    int dz;

    public Space(int maxx, int maxy, int maxz, int minx, int miny, int minz) {
        dx = minx-1;
        dy = miny-1;
        dz = minz-1;
        space = new bool[(maxx+1) - (minx-1) + 1, (maxy+1) - (miny-1) + 1, (maxz+1) - (minz-1) + 1];
    }

    public void visit((int x, int y, int z) point) {
        space[point.x-dx, point.y-dy, point.z-dz] = true;
    }

    public bool hasVisited((int x, int y, int z) point) {
        return space[point.x-dx, point.y-dy, point.z-dz];
    }
}

public class Point {
    public (int x, int y, int z) point;
    public int coveredSides = 0;
}