# Spring Annotation

## `@Bean`

**Using XML**

```xml
<bean id="person" class="com.spring.bean.Person">
	<Property name="age" value="18"></Property>
  <Property name="name" value="Tom"></Property>
</bean>
```

```java
public static void main(String[] args) {
  ApplicationContext applicationContext = new ClassPathXmlApplicationContext("beans.xml");
  Person person = applicationContext.getBean("person");
}
```

**Using annotation**

```java
@Configuration
public class MainConfig {
  // Bean's type is return type
  // Bean's id is method name by default
  @Bean
  // change bean's id
  // @Bean("Person1")
  public Person person() {
    return new Person("Tom", 18);
  }
}
```

```java
public static void main(Stirng[] args) {
  ApplicationContext applicationContext = new AnnotationConfigApplicationContext(MainConfig.class);
  Person person = applicationContext.getBean("person");
  // Person person = applicationContext.getBean(Person.class);
}
```

## `@ComponentScan`

**Using XML**

```xml
<!-- Scan components (@Controller, @Service & etc.) -->
<context:component-scan base-package="com.spring"></context:component-scan>
```

**Using annotation**

```java
@ComponentScan(value="com.spring")
public class MainConfig {
  // ...
}
```

### `excludeFilters`

exclude some components when scanning

```java
@ComponentScan(
  value="com.spring", 
  excludeFilters={
    @Filter(
      type=FilterType.ANNOTATION, 
      classes={Controller.class, Service.class}
    )})
```

### `includeFilters`

only include some components when scanning

```java
@ComponentScan(
  value="com.spring", 
  useDefaultFilters=false,
  includeFilters={
    @Filter(
      type=FilterType.ANNOTATION, 
      classes={Controller.class, Service.class}
    ), 
    @Filter(
        type=FilterType.ASSIGNABLE_TYPE, 
        classes={PersonRepository.class}
    )})
```

## `@Scope`

**values:** 

- `prototype` （多例）
- `singleton` **default**（单例）
- `request`
- `session`

```java
@Scope("prototype")
@Bean("Person")
public Person person() {
  // ...
}
```

## `@Lazy`

By default, **singleton bean** will be instantiated when the application runs. By `@Lazy` annotation, the bean will be instantiated when it's called. 

```java
@Lazy
@Bean("Person")
public Person person() {
  // ...
}
```

