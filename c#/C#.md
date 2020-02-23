# C# Notes

[TOC]

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

## Types of method parameters

- value parameters

  ```c#
  using System;
  class Program {
      public static void Main() {
          int i = 0;
          Method(i);
          Console.WriteLine(i);
          // result : 0
      }
      
      public static void Method(int j) {
          j = 100;
      }
  }
  ```

- reference parameters

  ```c#
  using System;
  class Program {
      public static void Main() {
          int i = 0;
          Method(ref i);
          Console.WriteLine(i);
          // result : 100
      }
      
      public static void Method(ref int j) {
          j = 100;
      }
  }
  ```

- out parameters

  ```c#
  using System;
  class Program {
      public static void Main() {
          int total = 0;
          int product = 0;
          Method(10, 20, out total, out product);
          Console.WriteLine("Sum = {0}, Product = {1}", total, product);
          // result : Sum = 30, Product = 200
      }
      
      public static void Method(int fn, int sn, out int sum, out int product) {
          sum = fn + sn;
          product = fn * sn;
      }
  }
  ```

- parameter arrays

  ```c#
  using System;
  class Program {
      public static void Main() {
          Method(1, 2, 3, 4);
          // result: 4
      }
      
      public static void Method(params int[] numbers) {
          Console.WriteLine(numbers.Lenght);
      }
  }
  ```

## namespace

```c#
using System;
using PATA = ProjectA.TeamA;

class Program {
    public static void Main() {
        PATA.ClassA.Print();
        // or ClassA.Print();
    }
}

namespace ProjectA {
    namespace TeamA {
        class ClassA {
            public static void Print() {
                Console.WriteLine("Team A print method");
            }
        }
    }
}
```

## Inheritance

```c#
public class Employee {
    string firstName;
    string lastName;
    string email;
    
    public void PrintFullName() {
        // do something
    }
}

public class FullTimeEmployee : Employee {
    float yearlySalary;
}

public class PartTimeEmployee : Employee {
    float hourlyRate;
}
```

```c#
public class Parent {
    public Parent() {}
    public Parent(string s) {}
}

public class Child : Parent {
    public Child() : base("message") {}
}
```



## Pillars of Object Oriented Programming

1. Inheritance
2. Encapsulation
3. Abstraction
4. Polymorphism

## Override

```c#
public class Employee {
    public virtual void Print() {
        Console.WriteLine("Employee");
    }
}

public class FullTimeEmployee : Employee {
    public override void Print() {
        Console.WriteLine("Full Time Employee");
    }
}
```

## Auto Implemented Properties

```c#
public class Employee {
    public string firstName {get; set;}
    public string lastName {get; set;}
}
```

## Properties

```c#
public class Employee {
    private string firstName;
    
    public string firstName {
        get { return this.firstName;}
        set { this,firstName = value;}
    }
}
```

## Object Initializer Syntax

```c#
using System;

public class Employee {
    public firstName { get; set;}
	public lastName { get; set;}
}

public class Program {
	public static void Main() {
        Employee e = new Employee {
            firstName = "fn", 
            lastName = "ln"
        };
    }
}
```

## Array

Single Dimension Arrays

```c#
var numbers = new int[5];
var numbers = new int[5]{1, 2, 3, 4, 5};
```

Multi Dimension Arrays: Rectangular & Jagged

![](F:\notes\c#\rectangular vs jagged.png)

Rectangular 2D:

```c#
var matrix = new int[3, 5];
var matrix = new int[3, 5] {
    {1, 2, 3, 4, 5},
    {6, 7, 8, 9, 10},
    {11, 12, 13, 14, 15}
};
var element = matrix[0, 0];
```

Rectangular 3D:

```c#
var colors = new int[3, 5, 3];
```

 Jagged:

```c#
var array = new int[3][];
array[0] = new int[4];
array[1] = new int[5];
array[2] = new int[3];
array[0][0] = 1;
```

methods of array

```c#
static void Main(String[] args) {
    var numbers = new[] {3, 5, 7, 9, 11};
    // Length
    Console.WriteLine("Length: {0}", numbers.Length);
    // Output: Length: 5
    
    // IndexOf()
    var index = Array.IndexOf(numbers, 9);
    Console.WriteLine("Index of 9: {0}", index);
    // Output: Index of 9: 3
    
    // Clear()
    Array.Clear(number, 0, 2);
    // value of numbers: {0, 0, 7, 9, 11}
    
    // Copy() 
    int[] another = new int[3];
    Array.Copy(numbers, another, 3);
    // value of another: {0, 0, 7}
    
    // Sort()
    var anotherNumbers = new[] {4, 3, 6};
    Array.Sort(numbers);
    // value of anotherNumbers: {3, 4, 6}
    
    // Reverse()
    Array.Reverse(anotherNumbers);
    // value of anotherNumbers: {6, 4, 3}
}
```

## Lists

### Creating Lists

```c#
var numbers = new List<int>();
var numbers = new List<int>() {1, 2, 3, 4, 5};
```

### Useful Methods

- Add()
- AddRange()
- Remove()
- RemoveAt()
- IndexOf()
- Contains()
- Count

### Example

```c#
static void Main(string[] args) {
    var numbers = new List<int>() {1, 2, 3, 4, 5};
    numbers.Add(1);
    numbers.AddRange(new int[3] {5, 6, 7});
    // now the value of numbers is {1, 2, 3, 4, 5, 1, 5, 6, 7}
    
    Console.WriteLine("Index of 1: {0}", numbers.IndexOf{1});
    // Output: Index of 1: 0
    
    Console.WriteLine("Last index of 1: {0}", numbers.LastIndexOf{1});
    // Output: Last index of 1: 5
    
    Console.WriteLine("Count: {0}", numbers.Count);
    // Output: Count: 9
    
    Console.Remove(1);
    // value of numbers: {2, 3, 4, 5, 1, 5, 6, 7}
    
    numbers.Clear();
    Console.WriteLine("Count: {0}", numbers.Count);
    // Output: Count: 0
}
```

## String

### Properties

#### Chars(Int32)

```c#
var str = "123";
char c = str[2]; // '3'
```

### Methods

#### Trim

```c#
var str = " abc ";
Console.WriteLine("'{0}'", str.Trim());
// output: 'abc'
```

#### ToUpper

```c#
var str = "abc";
Console.WriteLine(str.ToUpper());
// output: ABC
```

#### IndexOf

`IndexOf(char value)`

```c#
var str = "abc";
Console.WriteLine(str.IndexOf('a'));
// output: 0
```

#### Substring

1. `Substring(int startIndex)`

2. `Substring(int startIndex, int length)`

   the result will contain start index

```c#
var str = "abcdef";
Console.WriteLine(str.Substring(3));
Console.WriteLine(str.Substring(1,2));
// def
// bc
```

#### Split

`Split(char value)`

```c#
var str = "abc def";
var subStrings = str.Split(' ');
Console.WriteLine($"First: '{subStrings[0]}', Second: '{subStrings[1]}'");
// First: 'abc', Second: 'def'
```

#### Replace

1. `Replace(char oldValue, char newValue)`
2. `Replace(string oldValue, string? newValue)` 

```c#
var str = "abc def";
Console.WriteLine(str.Replace('a', '1'));
Console.WriteLine(str.Replace("ab", null));
// 1bc def
// c def
```

#### static IsNullOrEmpty

```c#
var b1 = String.IsNullOrEmpty(""); // true
var b2 = String.IsNullOrEmpty("   ".Trim()); // true
```

#### static IsNullOrWhiteSpace

```c#
var b3 = String.IsNullOrWhiteSpace("   "); // true
```

### Convert string to number/number to string

```c#
var str = "25";
var age = Convert.ToInt32(str); // 25

float price = 29.95f;
Console.WriteLine(price.ToString("C1")); // $ 30.0
```

## StringBuilder

used to manipulate string (saving memory)

### properties

#### Chars[Int32]

```c#
var sb = new StringBuilder("123");
char c = sb[0]; // '1'
```

### methods

1. Append
2. AppendLine
3. Replace
4. Remove
5. Insert

### Chain methods altogether

```c#
var sb = new StringBuilder("abc").Append("def").Insert(0, "1234").Replace('c', 'x');
Console.WriteLine(sb);
// 1234abxdef
```





 



