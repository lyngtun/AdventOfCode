// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");




var lines = System.IO.File.ReadLines(@"input.txt");

Node root = new Node();
Node? currentNode = root;

void parseCommand(string line) {
    var parts = line.Split(' ');

    if(parts[1] == "cd") {
        if(parts[2] == "/") {
            currentNode = root;
        }
        else if (parts[2] == "..") {
            currentNode = currentNode.parent;
        }
        else {
            foreach(var dir in currentNode.directories) {
                if(dir.name == parts[2]) {
                    currentNode = dir;
                    break;
                }
            }
        }
    }
}

foreach(var line in lines) {
    if(line.StartsWith("$")) {
        parseCommand(line);
    }
    else {
        var parts = line.Split(' ');
        if(parts[0] == "dir") {
            var dir = new Node();
            dir.parent = currentNode;
            dir.name = parts[1];
            currentNode.directories.Add(dir);
        }
        else {
            var file = new File { name=parts[1], size=Int32.Parse(parts[0]) };
            currentNode.files.Add(file);
        }
    }
}

var sumSmallDirs = 0;

int calculateDirSize(Node dir) {
    var sum = 0;

    foreach(var subdir in dir.directories) {
        sum += calculateDirSize(subdir);
    }

    foreach(var file in dir.files) {
        sum += file.size;
    }

    if(sum <= 100000) sumSmallDirs += sum;

    dir.size = sum;

    return sum;
}

int smallest = 70000000;
var totalSum = calculateDirSize(root);
var freeSpace = 70000000-totalSum;
var neededSpace = 30000000-freeSpace;

void findSmallestDirAbove(Node dir) {
    if(dir.size > neededSpace && dir.size < smallest) smallest = dir.size;

    foreach(var subdir in dir.directories) {
        findSmallestDirAbove(subdir);
    }
}


findSmallestDirAbove(root);

Console.WriteLine($"Total size: {totalSum}");
Console.WriteLine($"Needed size: {neededSpace}");
Console.WriteLine($"Smallest dir size: {smallest}");



public class File {
    public string name = "";
    public int size;
}

public class Node {
    public string name = "";
    public Node? parent;
    public List<Node> directories = new List<Node>();
    public List<File> files = new List<File>();
    public int size;
}
