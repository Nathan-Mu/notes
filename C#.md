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

