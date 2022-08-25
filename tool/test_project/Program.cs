// This will be the integer variable
// used as boolean container
int myBoolContainer = 1;

// 7th bit will be used for sample
int workingBit = 7;

// Setting ith bit
Console.WriteLine("Setting " + workingBit
                  + "th bit to 1");

myBoolContainer |= (1 << workingBit);

// Printing the ith bit
Console.WriteLine(
    "Value at " + workingBit + "th bit = "
    + ((myBoolContainer >> workingBit) & 1) + "\n");

// Resetting the ith bit
Console.WriteLine("Resetting " + workingBit
                  + "th bit to 0");

myBoolContainer &= ~(1 << 7);

// Printing the ith bit
Console.WriteLine(
    "Value at " + workingBit + "th bit = "
    + ((myBoolContainer >> workingBit) & 1));

Console.ReadLine();