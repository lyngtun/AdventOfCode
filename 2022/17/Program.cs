var moves = System.IO.File.ReadLines("input/inputexample.txt");
var allMoves = moves.First().ToCharArray();

var type1 = new char[4, 1];
for(int i=0; i<4; i++) {
    type1[i, 0] = '#';
}

var type2 = new char[3, 3];
for(int i=0; i<4; i++) {
    for(int j=0;j<3;j++) {
        type2[i, j] = '#';
    }
}
type2[0,0] = '.';
type2[0,2] = '.';
type2[2,0] = '.';
type2[2,2] = '.';

var type3 = new char[3, 3];
for(int i=0; i<4; i++) {
    for(int j=0;j<3;j++) {
        type3[i, j] = '#';
    }
}
type3[0,1] = '.';
type3[1,1] = '.';
type3[0,2] = '.';
type3[1,2] = '.';


var type4 = new char[1, 4];
for(int i=0; i<4; i++) {
    type4[0, j] = '#';
}

var type5 = new char[2, 2];

type5[0,0] = '#';
type5[0,1] = '#';
type5[1,0] = '#';
type5[1,1] = '#';



char [,] getRock(int typeNo) {
    switch(typeNo) {
        case 1: return type1;
        case 2: return type2;
        case 3: return type3;
        case 4: return type4;
        case 5: return type5;
    }

    throw new Exception();
}


var currentTop = 0;
const int maxwidth = 7;
var maxRocks = 2022;
var currentRock = 0;
var currentwind = 0;

List<char[]> chamber = new List<char[]>();

while(currentRock < maxRocks) {
    (int x, int y) pos = (2, currentTop+3);

    var currentType = (currentRock % 5)+1;
    var rock = getRock(currentRock);

    if(chamber.Count() < rock.GetLength(1)+pos.y) {
        // Add chamber lines
        for(int i=chamber.Count(); i<rock.GetLength(1)+pos.y; i++) {
            chamber.Add(new char[maxwidth] {'.','.','.','.','.','.','.'});
        }
    }

    var stopped = false;
    while(!stopped) {
        // Move
        pos = tryMoveRock(allMoves[currentwind++], currentType, pos);
        // Fall or stop
        if(canFallToLine(currentType, pos.x, pos.y-1)) {
            pos.y--;
        }
        else {
            addToLine(currentType, pos);
            stopped = true;
        }
    }

    currentRock++;
    currentTop = pos.y + rock.GetLength(1) > currentTop ? pos.y + rock.GetLength(1) : currentTop;
}

(int, int) tryMoveRock(char dir, int type, (int x, int y) pos) {
    var rock = getRock(type);

    if(dir == '<') {
        if(pos.x == 0) return pos;

        if(type == 1 && (chamber.ElementAt(pos.y)[pos.x-1]) == '.') return (pos.x-1, pos.y);
        if(type == 2 && (chamber.ElementAt(pos.y+1)[pos.x-1]) == '.') return (pos.x-1, pos.y);
        if(type == 3 && (chamber.ElementAt(pos.y)[pos.x-1]) == '.') return (pos.x-1, pos.y);
        
    }
    else {
        if(pos.x + rock.GetLength(0) >= 7) return pos;
    }

    return pos;
}

void addToLine(int type, (int x, int y) pos) {
    var rock = getRock(type);

    for(int i=0; i<rock.GetLength(1); i++) {
        var l = chamber.ElementAt(i);

        for(int j=0; j<rock.GetLength(0); j++) {
            if(rock[j, i] == '#') {
                l[pos.x+i] = '#';
            }
        }

        chamber.RemoveAt(i);
        chamber.Insert(i, l);
    }
}

bool canFallToLine(int type, int linePos, int line) {
    if(line < 0) return false;
    if(line > chamber.Count()) return true;
    if(chamber.Count == 0) return true;

    var l = chamber.ElementAt(line);
    switch(type) {
        case 1: return l[linePos] == '.' && l[linePos+1] == '.' && l[linePos+2] == '.' && l[linePos+3] == '.';
        case 2: return l[linePos+1] == '.';
        case 3: return l[linePos] == '.' && l[linePos+1] == '.' && l[linePos+2] == '.';
        case 4: return l[linePos] == '.';
        case 5: return l[linePos] == '.' && l[linePos+1] == '.';
    }

    throw new Exception();
}