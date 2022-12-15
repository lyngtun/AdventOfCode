using System.Text.RegularExpressions;

var lines = System.IO.File.ReadLines("input/input.txt");

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

var part1_y = 2000000;
var rowPositions = new HashSet<int>();

foreach(var sensor in sensors) {
    var c = GetSensorCoverage(sensor, part1_y);
    rowPositions.UnionWith(c);
}

foreach(var sensor in sensors) {
    if(sensor.BeaconPos.y == part1_y) rowPositions.Remove(sensor.BeaconPos.x);
}

Console.WriteLine($"Covered positions: {rowPositions.Count()}");


List<int> GetSensorCoverage(SensorData s, int rowNumber) {
    var result = new List<int>();

    var distance = Math.Abs(s.SensorPos.x - s.BeaconPos.x) + Math.Abs(s.SensorPos.y - s.BeaconPos.y);

    var dy = Math.Abs(s.SensorPos.y - rowNumber);
    var dr = distance - dy;

    for(int i=s.SensorPos.x - dr; i<=s.SensorPos.x + dr; i++) {
        result.Add(i);
    }

    return result;
}

public class SensorData {
    public (int x, int y) SensorPos;
    public (int x, int y) BeaconPos;
}