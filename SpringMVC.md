# SpringMVC

## `@RequestMapping`

### Where to put

put on the top of class and method declaration 

```java
@Controller
@RequestMapping("/helloWorld")
public class HelloWorld {
  @RequestMapping("/success")
  public String success() {
    return "success";
  }
}
```

### Attributes

- `value`
- `method`
- `params`
- `headers`

params and headers supports simple expressions, like

- `"name"`: must include a parameter called name
- `"!name"`: cannot include a parameter called name
- `"name!=abc"`: name's value must be "abc"
- `{"name=abc", "age"}`: must include name and age, and name's value must be "abc"

```java
@RequestMapping(value="/delete", method=RequestMehod.POST, params="userId", headers="contentType=text/*")
```



## `@PathVariable`

```java
@RequestMapping("/delete/{id}")
public String delete(@PathVariable("id") Integer id) {
// or @PathVariable(value="id")
  UserService.delete(id);
  // some code
}
```

## `@RequestParam`

### Attributes

- `value`
- `required`
- `defaultValue`

```java
@RequestMapping("/find")
public String find(
  @RequestParam("username") String userName, 
  @RequestParam(value="age", required=false, defaultValue="0") int age) {
  // ...
}
```

