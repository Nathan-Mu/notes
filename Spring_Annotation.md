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

## `@Conditional`

register beans based on conditions

## `@Import`

import a bean or beans with default constructer

```java
@Conditional({WindowsCondition.class})
@Configuration
@Import({Color.class, A.class, MyImportSelector.class})
public class MainConfig {
  // ...
}
```

```java
public class MyImportSelector implements ImportSelector {
  @Override
  public String[] selectImports(AnnotationMetadata importingClassMetadata) {
    return new String[] {"com.spring.B", "com.spring.C"};
  }
}
```



## 3 Ways to Register Components

1. component scan + component annotations (`@Controller`, `@Service` & etc.)
2. `@Bean` (import 3rd party components, normally calling **non-default constructer**)
3. `@Import` (fast import 3rd party components with **default constructer**)
4. `@Bean` + `FactoryBean` *(need more research...)*

## Lifecycle of a Bean

**init and destroy**

1. `@Bean(initMethod="xxx", destroyMethod="xxx")`

2. Implementing `InitializingBean` and `DisposableBean` 

3. `@PostConstruct` and `@PreDestroy`

4. Implementing `BeanPostProcesser` (be called before and after any bean is created)

   - `postProcessBeforeInitialization`

   - `postProcessAfterInitialization`

**Using xml**

```xml
<bean id="person" class="com.spring.bean.Person" init-method="xxxx" destory-method="xxxx"></bean>
```

**Using annotation**

1. `@Bean(initMethod="xxx", destroyMethod="xxx")`

```java
public class Person {
  public void init() {  }
  public void destroy() {  }
  // ...
}
```

```java
@Configuration
public class MainConfig {
  @Bean(initMethod="init", destroyMethod="destroy")
  public Person person() {
    return new Person()
  }
}
```

*A singleton bean will be destroyed automatically when the container closes, but prototype bean won't.*

2. `InitializingBean` and `DisposableBean` 

```java
@Component
public class Person implements InitializingBean, DisposableBean {
  @Override
  public void afterPropertiesSet() throws Exception {  }
  
  @Override
  public void destroy() throws Exception {  }
}
```

3. `@PostConstruct` and `@PreDestroy`

```java
@Component
public class Person {
  @PostConstruct
  public void init() {  }
  
  @PreDestroy
  public void destroy() {  }
}
```

