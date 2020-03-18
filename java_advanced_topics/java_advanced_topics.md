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

### Chaining exceptions

```java
public class AccountException extends Exception {
    public AccountException(Exception case) {
        super(cause);
    }
}
```

## 2. Generic

### Generic Classes

```java
public class MyList<T> {
    private T[] items = (T[]) new Object[10];
    private int count;
    
    public void add(T item) {
        items[count] = item;
        count++;
    }
}
```

### Constraints

```java
public class MyList<T extends Comparable & Cloneable> {
    private T[] items = (T[]) new Object[10];
    private int count;
    
    public void add(T item) {
        items[count] = item;
        count++;
    }
}
```

### Comparable Interface

```java
public class User implements Comparable<User> {
    // ... some code
    @Override
    public int compareTo(User other) {
        return this.points - other.points;
    }
}
```

### Generic Methods

```java
public static <T extends Comparable<T>> T max(T first, T second) {
    return (first.compareTo(second)) > 0 ? first : second;
}
```

### Wildcards

```java
public static void printUsers(List<? extends User> users) {
    // can only read from the list
    User x = users.get(0);
}

public static void addUsers(List<? super User> users) {
    users.add(new User("name"));
    users.add(new Instructor("name")); // Instructor extends User
}
```

## 3. Collection

### Overview

![](F:\notes\java_advanced_topics\collection_overview.png)

### Iterator

```java
public class MyList<T> implements Iterable<T> {
    // ... some code
    @Override
    public Iterator<T> iterator() {
        return new MyIterator(this);
    }
    
    private class MyIterator implements Iterator<T> {
        private MyList<T> list;
        private int index;
        
        public MyIterator(MyList<T> list) {
        	this.list = list;
        }
        
        @Override
        public boolean hasNext() {
            return (index < list.count);
        }
        
        @Override
        public T next() {
            return list.items[index++];
        }
    }
}
```

### Collection

```java
Collection<String> collection = new ArrayList<>();
collection.add("a");
collection.add("b");
Collections.addAll(collection, "c", "d");
int size = collection.size();
collection.remove("a");
collection.clear();
boolean isEmpty = collection.isEmpty;
boolean containsA = collection.contains("a");
String[] strings = collection.toArray(new String[]);
```

### List

```java
List<String> list = new ArrayList<>();
list.add("a");
list.add("b");
list.add(0, "!"); // [!, a, b]
Collections.addAll(list, "c", "a");
list.set(0, "!!"); // [!!, a, b, c, a]
String s = list.get(0);
list.remove(0);
int indexOfA = list.indexOf("a"); // return -1 if not found
int lastIndexOfA = list.lastIndexOf("a");
List<String> subList = list.subList(0, 2); // include 0, don't include 2
```

### Sorting

There 2 ways of sorting a list:

1. sorting a list of T which implements comparable interface
2. sorting a list of T by creating a new comparator

**Create a new comparator**

```java
public class EmailComparator implements Comparator<Customer> {
     @Override
     public int compare(Customer c1, Customer c2) {
         return c1.getEmail().compareTo(c2.getEmail());
     }
}
```

**Sort the list**

```java
List<Customer> customers = new ArrayList<>();
// ... add some customers
Collection.sort(customers, new EmailComparator);
System.out.println(customers);
```

### Queue

```java
Queue<String> queue = new ArrayDeque<>();
queue.add("c");
queue.add("a");
queue.add("b"); // [c, a, b]
// for ArrayDeque: offer() = add()
// for some interfaces which has a limited size, when the queue is full, using add() will throw an exception, and using offer() won't throw an exception
queue.offer("d"); // [c, a, b, d] 
// peek() return null if empty
String front = queue.peek(); // c
// element() throws an exception if empty
String front2 = queue.element(); // c
// poll() return null if empty
String front3 = queue.poll(); // remove the first element and remove
// remove() throws an exception if empty
String front4 = queue.remove(); // remove the first element and remove
```

### Set

Set stores unique values and doesn't guarantee the order

```java
Set<String> set1 = new HashSet<>(Arrays.asList("a", "b", "c"));
Set<String> set2 = new HashSet<>(Arrays.asList("b", "c", "d"));
// Union
set1.addAll(set2); // set1: [a, b, c, d]
// Intersection
set1.retainAll(set2); // set1: [b, c]
// Difference
set1.removeAll(set2); // set1: [a]
```

### Map

```java
Customer c1 = new Customer("a", "e1");
Customer c2 = new Customer("b", "e2");
Customer unknown = new Customer("unknown", "");
Map<String, Customer> map = new HashMap<>();
map.put(c1.getEmail(), c1);
map.put(c2.getEmail(), c2);
Customer target = map.get("e1"); // return c1
Customer target2 = map.get("e10"); // return null
Customer target3 = map.getOrDefault("e10"); // return unknown
boolean exists = map.containsKey("e10");
map.replace("e1", new Customer("aa", "e1"));
```

**Iterate a map**

```java
for (var key: map.keySet) {}
for (var value: map.valueSet) {}
for (var entry: map.entrySet) {}
```

## 4. Lambda Expressions

### Functional interface

```java
public interface Printer {
    void print(String message);
}
```

### Anonymous Class

```java
public class Demo {
    public static void main() {
        greet(new Printer {
            @Override
            public void print(String message) {
                System.out.println(message);
            }
        });
    }
    
    public static void greet(Printer printer) {
        printer.print("Hello world");
    }
}
```

### Lambda Expression

```java
public class Demo {
    public static void main() {
        greet(message -> System.out.println(message));
        // greet(System.out::println);
        Printer printer = message -> System.out.println(message);
    }
    
    public static void greet(Printer printer) {
        printer.print("Hello world");
    }
}
```

### Build-in Functional Interfaces

- Consumer : `void consume(obj)`
- Supplier :  `obj supply()`
- Function :  `obj map(obj)`
- Predicate : `bool test(condition)`

### Consumer Interface

```java
List<Integer> list = List.of(1, 2, 3);
// imperative
for (int item: list) {
    System.out.println(item);
}
// declarative
list.forEach(item -> System.out.println(item));
```

### Chaining consumer

```java
List<String> list = List.of("a", "b", "c");
Consumer<String> print = item -> System.out.println(item);
Consumer<String> printUpperCase = item -> System.out.println(item.toUpperCase());
list.forEach(print.andThen(printUpperCase));
```

### Supplier Interface

```java
Supplier<Double> getRandom = () -> Math.random();
var random = getRandom.get();
System.out.println(random);
```

### Function Interface

```java
Function<String, Integer> map = str -> str.length();
Integer length = map.apply("Sky");
System.out.println(length);
```

### Composing Functions

```java
Function<String, String> addPrefix = str -> "Name=" + str;
Function<String, String> addBraces = str -> "{" + str + "}";
String result = addPrefix.andThen(addBraces).apply("Tom");
result = addBraces.compose(addPrefix).apply("Tom");
System.out.println(result);
```

### Predicate Interface

```java
Predicate<String> isLongerThan5 = str -> str.length() > 5;
boolean result = isLongerThan5.test("sky");
```

### Combining Predicates

```java
Predicate<String> hasLeftBrace = str -> str.startsWith("{");
Predicate<String> hasRightBrace = str -> str.endsWith("}");
boolean hasLeftAndRightBrace = hasLeftBrace.and(hasRightBrace).test("{}");
boolean hasLeftOrRightBrace = hasLeftBrace.or(hasRightBrace).test("{}");
boolean notHasLeft = hasLeftBrace.negate().test("{}");
```

### BinaryOperator & UnaryOperator

```java
BinaryOperator<Integer> add = (a, b) -> a + b;
UnaryOperator<Integer> square = a -> a * a;
Integer result = add.andThen(square).apply(1, 2); 
```

## 5. Streams

### Imperative Programming

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 15),
    new Movie("c", 20)
);

int count = 0;
for (Movie movie: movies) {
    if (movie.getLikes() > 10)
        count++;
}
```

### Declarative (Functional) Programming

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 15),
    new Movie("c", 20)
);

int count = movies.stream()
    			.fliter(movie -> movie.getLikes() > 10)
    			.count();
```

