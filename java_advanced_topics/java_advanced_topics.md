# Java advanced topics

## 1. Exceptions

### Three Types of Exceptions

- Checked exception 
  - *developers should anticipate and handle properly*
  - *Java compiler enforces us to handle these errors*
  - *e.g. `IOException`*
- Unchecked exception
  - *also called runtime exception*
  - *not checked by the compiler at compile time*
  - *e.g. `NullPointerException`, `IndexOutOfBoundsException`*
- Error
  - *happens outside the application*
  - *e.g. out of memory*

### Exception Hierarchy

![](F:\notes\java_advanced_topics\exception_hierarchy.png)

### Catching Exceptions

```java
try {
    FileReader reader = new FileReader("file.txt");
} catch (FileNotFoundException e) {
    System.out.println(e.getMessage());
}
```

### Catching Multiple Types of Exceptions

**try:**

```java
try {
    FileReader reader = new FileReader("file.txt"); // FileNotFoundException (which extends IOException)
    int value = reader.read(); // IOException
    int anotherValue = Integer.parseInt("abc"); // NumberFormatException
}
```

**catch:**

one solution

```java
catch (FileNotFoundException e) {
    System.out.println("File not found");
} catch (IOException e) {
    System.out.println("File cannot be read");
} catch (NumberFormatException e) {
     System.out.println("Parse exception");
 }
```

or another solution

```java
catch (IOException | NumberFormatException e) {
    System.out.println(e.getMessage());
}
```

### Finally Block

```java
FileReader reader;
try {
    reader = new FileReader("file.txt");
    int value = reader.read();
} catch (FileNotFoundException e) {
    System.out.println(e.getMessage());
} finally {
    try {
        if (reader != null)
        	reader.close();
    } catch (IOException e) {
        System.out.println(e.getMessage());
    }
}
```

### try-with-resources

- no need to close the resource in finally
- **only** working with classes that implements `autoClosable` interface
- will automatically close the resources

```java
try (
    FileReader reader = new FileReader("file.txt");
    FileWriter writer = new FileWriter("...")
) {
    int value = reader.read();
} catch (IOException e) {
    System.out.println(e.getMessage());
}
```

