// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var foodList = new List<int>();
var currentFood = 0;

foreach (string line in System.IO.File.ReadLines(@"input1.txt"))
{  
    // Console.WriteLine($"Read line: '{currentFood}'");
    if (String.IsNullOrWhiteSpace(line)) {
        Console.WriteLine($"Elf carries: '{currentFood}'");
        foodList.Add(currentFood);
        currentFood = 0;
    }
    else {
        var lineFood = Int32.Parse(line);
        // Console.WriteLine($"Found value: {lineFood}");
        currentFood += lineFood;
        // Console.WriteLine($"New total: {currentFood}");
    }
}  

foodList.Sort();
foodList.Reverse();

var threeMax = foodList[0] + foodList[1] + foodList[2];

Console.WriteLine($"Max 3 food is {threeMax}");