# C# Notes

## Reading and writing to console

```c#
using System;
class Program {
	static void main() {
		// Prompt the user for his name
		Console.WriteLine("Please enter your name");
        // Read the name from console
        string UserName = Console.ReadLine();
        // Concatenate name with hello world and print
        Concole.WriteLine("Hello " + UserName)
        // Place holder syntax to print name
        // Console.WriteLine("Hello {0}", UserName);
	}
}
```

## Built-in Data Type

- Boolean - bool
- Integral - char, int, long
- Floating-point - float, double
- Decimal - decimal
- String - string

## String (backslash)

```c#
string s = "C:\\Nathan\\Documents";
```

or with verbatim literal

```c#
string s = @"C:\Nathan\Documents";
```

**Verbatim literal**, is a string with an @ symbol prefix, as in @"Hello". **Verbatim literals** make escape sequences translate as normal printable characters to enhance readability. 

## Ternary Operator (? :)

## Types in C# 

**In C# types are divided into 2 broad categories**

**Value Types** - int, float, double, etc.

**Reference Types** - Interface, Class, delegates, array, etc. 



**By default value types are non nullable.** 

```c#
int i = 0; // i is non nullable, so i = null will generate compiler error
int? j = 0 // j is nullable int, so j = null is legal
```

## Null Coalescing Operator ??

```
int? i = 100;
int j = i ?? 0;
```

## Conversion

```c#
string strNumber = "100TG";
int result = 0;
bool isConversionSuccessful = int.TryParse(strNumber, out result);
```

```c#
float f = 111.111;
int i = Convert.ToInt32(f);
```

## Switch

```c#
switch (number) {
    case 1: 
        Console.WriteLine("Your number is 1"); break;
    case 2:
        Console.WriteLine("Your number is 2"); break;
    default:
        Console.WriteLine("Your number is not 1 or 2"); break;
}
```

```c#
switch (number) {
	case 1:
	case 2:
		Console.WriteLine("Your number is {0}", number); break;
	default:
        Console.WriteLine("Your number is not 1 or 2"); break;
}
```

## goto & break

```c#
Start:
Console.WriteLine("1 - Large, 2 - Small");
switch (number) {
	case 1:
	case 2:
		Console.WriteLine("Your number is {0}", number); break;
	default:
        Console.WriteLine("Your number is not 1 or 2");
        goto Start;
        // goto case 1;
        break;
}
Decide:
Console.WriteLine("Repeat? - yes or no?");
string userDecision = Console.ReadLine();
switch (userDecision) {
    case "yes":
        goto Start;
    case "no":
        break;
    default: 
        Console.WriteLine("Invalid option. Please enter again.");
        goto Decide;
}

```

**break statement:** If break statement is used inside a switch statement, the control will leave the switch statement.

**goto statement:** You can either jump to another case statement, or to a specific label. **Using goto statement is a bad programming style. Avoid it.** 

## while loop

```c#
int i = 0;
while (i <= 10) {
	Console.WriteLine(i);
	i = i + 2;
}
```

## do ... while loop

```c#
string userChoice = "";
do {
	Console.WriteLine("Do you want to continue - Yes or No?");
	userChoice = Console.ReadLine();
	if (userChoice != "Yes" && userChoice != "No") {
		Console.WriteLine("Invalid input");
	} else if (userChoice == "Yes") {
		// do something
	}
} while (UserChoice != "Yes" && UserChoice != "No");
```

## for loop

```c#
for (int i = 0; i < 10; i++) {
	Console.WriteLine(i);
}
```

## for each loop

```c#
int[] numbers = {1, 2, 3};
foreach (int i in numbers) {
	Console.WriteLine(i);
}
```













