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
String s = "C:\\Nathan\\Documents";
```

or with verbatim literal

```c#
String s = @"C:\Nathan\Documents";
```

**Verbatim literal**, is a string with an @ symbol prefix, as in @"Hello". **Verbatim literals** make escape sequences translate as normal printable characters to enhance readability. 