using System.Text.RegularExpressions;

var lines = System.IO.File.ReadLines("input/input.txt");
// var part1_y = 10;
// var part2_max = 20;
var part1_y = 2000000;
var part2_max = 4000000;

string pattern = @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)";
Regex rg = new Regex(pattern);

var sensors = new List<SensorData>();

int minx = Int32.MaxValue;
int miny = Int32.MaxValue;
int maxx = Int32.MinValue;
int maxy = Int32.MinValue;

foreach(var sensor in lines) {
    var match = rg.Match(sensor);

    var sx = Int32.Parse(match.Groups[1].Value);
    var sy = Int32.Parse(match.Groups[2].Value);
    var bx = Int32.Parse(match.Groups[3].Value);
    var by = Int32.Parse(match.Groups[4].Value);

    if(sx < minx || bx < minx) minx = (sx < bx) ? sx : bx;
    if(sy < miny || by < miny) miny = (sy < by) ? sy : by;
    if(sx > maxx || bx > maxx) maxx = (sx > bx) ? sx : bx;
    if(sy > maxy || by > maxy) maxy = (sy > by) ? sy : by;

    sensors.Add(new SensorData { 
        SensorPos = (sx, sy),
        BeaconPos = (bx, by)
    });
}

var rowPositions = new HashSet<int>();

foreach(var sensor in sensors) {
    var c = GetSensorCoverage(sensor, part1_y);
    rowPositions.UnionWith(c);
}

foreach(var sensor in sensors) {
    if(sensor.BeaconPos.y == part1_y) rowPositions.Remove(sensor.BeaconPos.x);
}

Console.WriteLine($"Part 1 positions: {rowPositions.Count()}");


var checkPoints = new HashSet<(int, int)>();

foreach(var sensor in sensors) {
    checkPoints.UnionWith(GetAjacentPositions(sensor));
}

var part2Pos = (-1, -1);
foreach(var point in checkPoints) {
    var found = false;
    foreach(var sensor in sensors) {
        if(isCovered(point, sensor)) {
            found = true;
            break;
        }
    }

    if(!found) {
        part2Pos = point;
        break;
    }
}


var freq = (Int64)4000000 * (Int64)part2Pos.Item1 + (Int64)part2Pos.Item2;

Console.WriteLine($"Part 2 position {part2Pos}, freq {freq}");

bool isCovered((int x, int y) point, SensorData s) {
    if(point.x<0 || point.y<0 || point.x>part2_max || point.y>part2_max) return true;
    var distance = Math.Abs(s.SensorPos.x - s.BeaconPos.x) + Math.Abs(s.SensorPos.y - s.BeaconPos.y);
    var pd = Math.Abs(s.SensorPos.x - point.x) + Math.Abs(s.SensorPos.y - point.y);

    return pd <= distance;
}

List<(int, int)> GetAjacentPositions(SensorData s) {
    var distance = Math.Abs(s.SensorPos.x - s.BeaconPos.x) + Math.Abs(s.SensorPos.y - s.BeaconPos.y);
    var res = new List<(int, int)>();

    for(var i=s.SensorPos.y-distance; i <= s.SensorPos.y+distance; i++) {
        var dx = distance - Math.Abs(s.SensorPos.y - i) + 1;
        res.Add((s.SensorPos.x-dx, i));
        res.Add((s.SensorPos.x+dx, i));
    }

    res.Add((s.SensorPos.x, s.SensorPos.y-distance-1));
    res.Add((s.SensorPos.x, s.SensorPos.y+distance+1));

    return res;
}


List<int> GetSensorCoverage(SensorData s, int rowNumber, int maxValue = Int32.MaxValue, int minValue = Int32.MinValue) {
    var result = new List<int>();

    var distance = Math.Abs(s.SensorPos.x - s.BeaconPos.x) + Math.Abs(s.SensorPos.y - s.BeaconPos.y);

    var dy = Math.Abs(s.SensorPos.y - rowNumber);
    if(dy > distance) return result;

    var dr = distance - dy;

    for(int i=s.SensorPos.x - dr; i<=s.SensorPos.x + dr; i++) {
        if(i <= maxValue && i >= minValue) result.Add(i);
    }

    return result;
}

public class SensorData {
    public (int x, int y) SensorPos;
    public (int x, int y) BeaconPos;
}