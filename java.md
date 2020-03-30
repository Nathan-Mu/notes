# Java advanced topics

[TOC]

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

![](https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/java/exception_hierarchy.png)

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

![](https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/java/collection_overview.png)

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

### Creating Streams

- From collections
- From arrays
- From an arbitrary number of objects
- Infinite/finite streams

**From arrays**

```java
int[] numbers = {1, 2, 3};
Stream stream = Arrays.stream(numbers);
```

**From an arbitrary number of objects**

```java
Stream stream = Stream.of(1, 2, 3, 4);
```

**Infinite/finite stream**

`Stream.generate()`

```java
Stream stream = Stream.generate(() -> Math.random());
stream
    .limit(3)
    .forEach(n -> System.out.println(n));
```

`Stream.iterate()`

```java
Stream.iterate(1, n -> n + 1)
    .limit(3)
    .forEach(n -> System.out.println(n));
```

### Mapping Elements

```java
movies.stream()
    	.map(movie -> movie.getTitle())
    	.forEach(name -> System.out.println(name));
```

`flatMap()`

```java
// Stream<List<X>> -> Stream<X>
Stream<List<Integer>> stream = Stream.of(List.of(1, 2, 3), List.of(4, 5, 6));
stream
    .flatMap(list -> list.stream())
    .forEach(n -> System.out.println(n));
```

### Filtering Elements

```java
Predicate<Movie> isPopular = movie -> movie.getLikes() > 10;
movies.stream()
    .filter(isPopular)
    .forEach(movie -> System.out.println(movie.getTitle()));
```

### Slicing Streams

- `limit(n)`
- `skip(n)`
- `takeWhile(predicate)`
- `dropWhile(predicate)`

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 15),
    new Movie("c", 20)
);

Consumer<String> printTitle = movie -> System.out.println(movie.getTitle());

movies.stream()
    .limit(2)
    .forEach(printTitle);
// a
// b

movies.stream()
    .skip(2)
    .forEach(printTitle);
// c

movies.stream()
    .takeWhile(movie -> movies.getLikes() < 20)
    .forEach(printTitle);
// a
// b

movies.stream()
    .dropWhile(movie -> movies.getLikes() < 20)
    .forEach(printTitle);
// c
```

### Sorting Streams

```java
List<Movie> movies = List.of(
	new Movie("b", 10),
    new Movie("a", 15),
    new Movie("c", 20)
);

movies.stream()
    // .sorted((a, b) -> a.getTitle().compareTo(b.getTitle()))
    // .sorted(Comparator.comparing(m -> m.getTitle()))
    .sorted(Comparator.comparing(Movie::getTitle))
    // .sorted(Comparator.comparing(Movie::getTitle)).reversed()
    .forEach(m -> System.out.println(m.getTitle()));
```

### Getting Unique Elements

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 10),
    new Movie("c", 20)
);

movies.stream()
    .map(Movie::getLikes)
    .distinct()
    .forEach(System.out::println);
```

### Peeking Elements

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 20),
    new Movie("c", 20)
);

movies.stream()
    .filter(m -> m.getLikes() > 10)
    .peek(m -> System.out.println("filtered: " + m.getTitle()))
    .map(Movie::getTitle)
    .peek(t -> System.out.println("mapped: " + t))
    .forEach(System.out::println);

// filtered: b
// mapped: b
// b
// filtered: c
// mapped: c
// c
```

### Intermediate Operations

- `map()`/`flatMap()`
- `filter()`
- `limit()`/`skip()`
- `sorted()`
- `distinct()`
- `peek()`

### Reducers

- `count()`
- `anyMatch(predicate)`
- `allMatch(predicate)`
- `noneMatch(predicate)`
- `findFirst()`
- `findAny()`
- `max(comparator)`
- `min(comparator)`

```java
int count = movies.stream().count();
boolean b1 = movies.stream().anyMatch(m -> m.getLikes() > 20);
boolean b2 = movies.stream().allMatch(m -> m.getLikes() > 20);
boolean b3 = movies.stream().noneMatch(m -> m.getLikes() > 20);
Optional<Movie> movie = movies.stream().findFirst();
```

### Reducing a Stream

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 20),
    new Movie("c", 20)
);
Optional<Integer> sum = movies.stream()
                            .map(m -> m.getLikes())
                            .reduce((a, b) -> a + b);
System.out.println(sum.orElse(0)); //60
```

### Collectors

```java
List<Movie> movies = List.of(
	new Movie("a", 10),
    new Movie("b", 20),
    new Movie("c", 20)
);

List list = movies.stream()
    			.filter(m -> m.getLikes() > 10)
    			.collect(Collectors.toList());

Set set = movies.stream()
            .filter(m -> m.getLikes() > 10)
            .collect(Collectors.toSet());

Map map = movies.stream()
            .filter(m -> m.getLikes() > 10)
            // .collect(Collectors.toMap(m -> m.getTitle, m -> m))
    		.collect(Collectors.toMap(Movie::getTitle, Function.identity()));

int sum = movies.stream()
            .filter(m -> m.getLikes() > 10)
            .collect(Collectors.summingInt(Movie::getLikes));

var result = movies.stream()
    			.filter(m -> m.getLikes() > 10)
    			.collect(Collectors.summarizingInt(Movie::getLikes));
// IntSummaryStatics{count=2, sum=50, min=20, average=25.000000, max=30}

String string = movies.stream()
    				.filter(m -> m.getLikes() > 10)
    				.map(Movie::getTitle)
    				.collect(Collectors.joining(", "));
// b, c
```

### Grouping Elements

```java
List<Movie> movies = List.of(
	new Movie("a", 10, Genre.THRILLER),
    new Movie("b", 20, Genre.ACTION),
    new Movie("c", 20, Genre.ACTION)
);

Map<Genre, List<Movie>> result = movies.stream()
    	.collect(Collectors.groupingBy(Movie::getGenre));

Map<Genre, String> result2 = movies.stream()
    	.collect(Collectors.groupingBy(
        	Movie::getGenre,
            Collectors.mapping(
            	Movie::getTitle,
                Collectors.joining(", ")
            )
        ));
// {ACTION=b, c, THRILLER=a}

Map<Genre, Integer> = movies.stream()
    	.collect(Collectors.groupingBy(
        	Movie::getGenre, Collectors.counting()
        ));
// {THRILLER=1, ACTION=2}
```

### Partitioning Elements

```java
Map<Boolean, List<Movie>> result = movies.stream()
    .collect(Collectors.partitioningBy(m -> m.getLikes() > 20));

Map<Boolean, String> result2 = movies.stream()
    .collect(Collectors.partitioningBy(
        m -> m.getLikes() > 20), 
        Collectors.mapping(
        	Movie::getTitle, 
            Collectors.join(",")
        )
   	);
// {false=a, b, true=c}
```

### Primitive Type Stream

- `IntStream`
- `LongStream`
- `DoubleStream`

```java
IntStream.range(1, 3).forEach(System.out::println);
// 1
// 2

IntStream.rangeClosed(1, 2).forEach(System.out::println);
// 1
// 2
```

