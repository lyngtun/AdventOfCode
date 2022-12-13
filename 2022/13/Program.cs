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

string printList(List<PacketData> dataList) {
    var retVal = "[";

    foreach(var data in dataList) {
        if(data.listVal == null) {
            retVal += data.intVal + ",";
        }
        else {
            retVal += printList(data.listVal) + ",";
        }
    }

    // Remove last comma
    if(dataList.Count() > 0) {
        retVal = retVal.Remove(retVal.Length - 1);
    }

    retVal += "]";
    return retVal;
}

int isRightOrder(List<PacketData> left, List<PacketData> right) {
    Console.WriteLine("Left:  " + printList(left));
    Console.WriteLine("Right: " + printList(right));
    for(var i=0; ; i++) {
        if(left.Count() <= i) {
            // Left ran out of items
            return (left.Count() < right.Count()) ? 1 : 0;
        }
        else if(right.Count() <= i) {
            // Right side ran out of items
            return (left.Count() > right.Count()) ? -1 : 0;
        }
        else if(left[i].listVal == null && right[i].listVal == null) {
            if(left[i].intVal > right[i].intVal) {
                // Right smaller, not right order
                return -1;
            }
            if(left[i].intVal < right[i].intVal) {
                // Left smaller, right order
                return 1;
            }
        }
        else if(left[i].listVal != null && right[i].listVal != null) {
            // Compare new lists
            var retVal = isRightOrder(left[i].listVal, right[i].listVal);
            if(retVal != 0) {
                return retVal;
            }
        }
        else if(left[i].listVal != null && right[i].listVal == null) {
            // Right is number, convert to list
            var tmpList = new List<PacketData>();
            tmpList.Add(new PacketData(right[i].intVal, null));
            var retVal = isRightOrder(left[i].listVal, tmpList);
            if(retVal != 0) {
                return retVal;
            }
        }
        else if(left[i].listVal == null && right[i].listVal != null) {
            // Left is number
            var tmpList = new List<PacketData>();
            tmpList.Add(new PacketData(left[i].intVal, null));
            var retVal = isRightOrder(tmpList, right[i].listVal);
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
    if(isRightOrder(set.left, set.right) > 0) {
        Console.WriteLine($"Set {currentSet} in right order.");
        okSetSum += currentSet;
    }
}

Console.WriteLine($"Sum of right order sets: {okSetSum}");

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