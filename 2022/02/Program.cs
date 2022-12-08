// See https://aka.ms/new-console-template for more information
Console.WriteLine("Day 2!");

var score = 0;

int getShapePoints(string myChoice) {
    switch(myChoice) {
        case "X":
            return 1;
        case "Y":
            return 2;
        case "Z":
            return 3;
    }

    throw new System.Exception();
}

int getWinScore(string opponentChoice, string myChoice) {
    switch(opponentChoice) {
        case "A":
            switch(myChoice) {
                case "X": 
                    return 3;
                case "Y":
                    return 6;
                case "Z":
                    return 0;
            }
            break;
        case "B":
            switch(myChoice) {
                case "X": 
                    return 0;
                case "Y":
                    return 3;
                case "Z":
                    return 6;
            }
            break;
        case "C":
            switch(myChoice) {
                case "X": 
                    return 6;
                case "Y":
                    return 0;
                case "Z":
                    return 3;
            }
            break;
    }
    throw new System.Exception();
}

string getMyChoice(string opponentChoice, string outcome) {
    switch(opponentChoice) {
        case "A":
            switch(outcome) {
                case "X": 
                    return "Z";
                case "Y":
                    return "X";
                case "Z":
                    return "Y";
            }
            break;
        case "B":
            switch(outcome) {
                case "X": 
                    return "X";
                case "Y":
                    return "Y";
                case "Z":
                    return "Z";
            }
            break;
        case "C":
            switch(outcome) {
                case "X": 
                    return "Y";
                case "Y":
                    return "Z";
                case "Z":
                    return "X";
            }
            break;
    }
    throw new System.Exception();
}

int getScore(string outcome) {
    switch(outcome) {
        case "X": return 0;
        case "Y": return 3;
        case "Z": return 6;
    }
    throw new System.Exception();
}

foreach (string line in System.IO.File.ReadLines(@"input1.txt"))
{
    var choices = line.Split(' ');
    var opponentChoice = choices[0].Trim();
    var outcome = choices[1].Trim();

    var myChoice = getMyChoice(opponentChoice, outcome);
    var points = getShapePoints(myChoice) + getScore(outcome);

    score += points;
    Console.WriteLine($"Elf: {opponentChoice} Outcome: {outcome} Me: {myChoice} Points: {points}  Total score: {score}");
}