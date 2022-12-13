var lines = System.IO.File.ReadAllLines("input/input.txt");

int parsePos = 0;

List<PacketData> readPacketData(string line) {
    var retVal = new List<PacketData>();

    if(line[parsePos++] != '[') {
        throw new Exception($"Not reading list start at position {parsePos}");
    }

    while(line[parsePos] != ']') {
        if(char.IsDigit(line[parsePos])) {
            string val = "";
            do {
                val += line[parsePos++];
            } while (char.IsDigit(line[parsePos]));

            retVal.Add(new PacketData(Int32.Parse(val), null));
        }
        else if(line[parsePos] == ',') {
            parsePos++;
        }
        else if(line[parsePos] == '[') {
            retVal.Add(new PacketData(0, readPacketData(line)));
            parsePos++;
        }
    }

    return retVal;
}

int comparePacketLists(List<PacketData> left, List<PacketData> right) {
    for(var i=0; ; i++) {
        if(left.Count() <= i) {
            // Left ran out of items
            return (left.Count() < right.Count()) ? -1 : 0;
        }
        else if(right.Count() <= i) {
            // Right side ran out of items
            return (left.Count() > right.Count()) ? 1 : 0;
        }
        else if(left[i].listVal == null && right[i].listVal == null) {
            if(left[i].intVal > right[i].intVal) {
                // Right smaller, not right order
                return 1;
            }
            if(left[i].intVal < right[i].intVal) {
                // Left smaller, right order
                return -1;
            }
        }
        else if(left[i].listVal != null && right[i].listVal != null) {
            // Compare new lists
            var retVal = comparePacketLists(left[i].listVal, right[i].listVal);
            if(retVal != 0) {
                return retVal;
            }
        }
        else if(left[i].listVal != null && right[i].listVal == null) {
            // Right is number, convert to list
            var tmpList = new List<PacketData>();
            tmpList.Add(new PacketData(right[i].intVal, null));
            var retVal = comparePacketLists(left[i].listVal, tmpList);
            if(retVal != 0) {
                return retVal;
            }
        }
        else if(left[i].listVal == null && right[i].listVal != null) {
            // Left is number
            var tmpList = new List<PacketData>();
            tmpList.Add(new PacketData(left[i].intVal, null));
            var retVal = comparePacketLists(tmpList, right[i].listVal);
            if(retVal != 0) {
                return retVal;
            }
        }
    }
}

var packetSets = new List<PacketSet>();

for(var i=0; i<lines.Count(); i++) {
    parsePos = 0;
    var leftPacket = readPacketData(lines[i++]);
    parsePos = 0;
    var rightPacket = readPacketData(lines[i++]);

    packetSets.Add(new PacketSet(leftPacket, rightPacket));
}

var currentSet = 0;
var okSetSum = 0;
foreach(var set in packetSets) {
    currentSet++;
    if(comparePacketLists(set.left, set.right) < 0) {
        okSetSum += currentSet;
    }
}

Console.WriteLine($"Part 1: Sum of right order sets: {okSetSum}");

// Add part 2 packets
var divPack1Item = new PacketData(2, null);
var divPack1 = new PacketData(0, new List<PacketData> { divPack1Item });
var divPack1List = new List<PacketData> { divPack1 };

var divPack2Item = new PacketData(6, null);
var divPack2 = new PacketData(0, new List<PacketData> { divPack2Item });
var divPack2List = new List<PacketData> { divPack2 };

packetSets.Add(new PacketSet(divPack1List, divPack2List));

var sortedPackets = new List<List<PacketData>>();

foreach(var set in packetSets) {
    sortedPackets.Add(set.left);
    sortedPackets.Add(set.right);
}

sortedPackets.Sort(comparePacketLists);

var index1 = sortedPackets.IndexOf(divPack1List)+1;
var index2 = sortedPackets.IndexOf(divPack2List)+1;
var p = index1*index2;

Console.WriteLine($"Part 1: 1st divider at {index1} 2nd divider at {index2}, product: {p}");

public class PacketSet {
    public List<PacketData> left;
    public List<PacketData> right;

    public PacketSet(List<PacketData> left, List<PacketData> right) {
        this.left = left;
        this.right = right;
    }
}

public class PacketData {
    public int intVal = 0;
    public List<PacketData>? listVal = null;

    public PacketData(int val, List<PacketData>? data) {
        intVal = val;
        listVal = data;
    }
}