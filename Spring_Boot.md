# Spring Boot

## 1. Hello World

### 开发

```java
@SpringBootApplication
public class HelloWorld {
  public static void main(String[] args) {
    // 启动主程序
    SpringApplication.run(HelloWorld.class, args);
  }
}
```

```java
@Controller
public class HelloWorldController {
  @ResponseBody
  @RequestMapping("/hello")
  public String hello() {
    return "Hello World";
  }
}
```

`@SpringBootApplication` 

标注主程序类

`@ResponseBody`

将Controller方法返回的对象，通过适当的`HttpMessageConverter`转换为指定格式后，写入到Response对象的body数据区



`@RestController`

```java
@RestController //@ResponseBody + @Controller
public class HelloWorldController {

  @RequestMapping("/hello")
  public String hello() {
    return "Hello World";
  }
}
```



### 部署

导入Maven插件 -> 将应用打包成一个可执行的jar包

```xml
<build>
    <plugins>
        <plugin>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-maven-plugin</artifactId>
        </plugin>
    </plugins>
</build>
```

通过`maven package`打包后，执行`java -jar hello-world.jar`即可运行

## 2. 默认的Spring Boot项目

 📁 demo
├── 📄 pom.xml
└── 📁 src
    ├── 📁 main
     |   ├── 📁 java
     |    |   └── 📦 com.example
     |    |       └── 📄 DemoApplication.java
     |   └── 📁 resources
     |       ├── 📄 application.properties: Spring Boot应用的配置文件
     |       ├── 📁 static: 静态资源，如css, js等
     |       └── 📁 templates: 页面，Spring Boot不支持jsp，支持模板引擎，如freemarker，thymeleaf
    └── 📁 test
        └── 📁 java
            └── 📦 com.example
                 └── 📄 DemoApplicationTests.java