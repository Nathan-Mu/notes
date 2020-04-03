# JPA

## What is JPA?

- Java Persistence API （对于对象持久化的API）
- JPA是ORM规范
- Hibernate是ORM框架，对JPA的实现

##  使用JPA持久化对象的步骤

1. 创建`persistence.xml`, 在这个文件中配置持久化单元
2. 创建实体类，使用注解来描述实体类跟数据库表之间的关系
3. 使用 JPA API 完成数据的增删改查
   - 创建`EntityManagerFactory`（对应Hibernate中的`SessionFactory`）
   - 创建`EntityManager`(对应Hibernate中的`Session`)

## JPA基本注解

**类注解**

- `@Entity`
- `@Table`



**属性/getter注解**

- `@Id`
- `@GeneratedValue`
- `@Column`
- `@Basic`
- `@Transient`
- `@Temporal`



### `@Entity`

- 标注在实体类前，指出该Java 类为实体类，将映射到指定的数据库表
- 如声明一个实体类 Customer，它将映射到数据库中的 customer 表上。

### `@Table`

- 当实体类与其映射的数据库表名不同时，需要使用该标注说明
- 该标注与 @Entity 标注并列使用
- 置于实体类声明语句之前
- 属性：
  - `name`：指明数据库的表名
  - `catalog` 和 `schema`：设置表所属的数据库目录或模式，通常为数据库名
  - `uniqueConstraints` ：设置约束条件，通常不须设置。

### `@Id`

- 用于声明一个实体类的属性映射为数据库的主键列
- 通常置于属性声明语句之前
- 也可置于属性的getter方法之前

### `@GeneratedValue`

- 用于标注主键的生成策略，通过 strategy 属性指定
- 默认情况下，JPA 自动选择一个最适合底层数据库的主键生成策略：SqlServer 对应 identity，MySQL 对应 auto increment。
- 供选择`strategy`：
  - `GenerationType.IDENTITY`：自增，Oracle 不支持这种方式
  - `GenerationType.AUTO`: 默认，JPA自动选择合适的策略
  - `GenerationType.SEQUENCE`：通过sequence产生主键，通过 `@SequenceGenerator` 注解指定序列名，MySql 不支持这种方式
  - `GenerationType.TABLE`: 通过表产生主键，框架借由表模拟序列产生主键，使用该策略可以使应用更易于数据库移植

### `@Basic`

- 表示一个简单的属性到数据库表的字段的映射
- 对于没有任何标注的 getXxxx() 方法,默认即为@Basic
- `fetch`: 表示该属性的读取策略( `EAGER` & `LAZY` )，默认为 EAGER.
- `optional`: 表示该属性是否允许为null, 默认为true

### `@Column`

- 当实体的属性与其映射的数据库表的列不同名时使用
- 可与 `@Id` 一起使用
- 属性：
  - `name`: 设置映射数据库表的列名
  - `unique`
  - `nullable`
  - `length`
  - `columnDefinition`

### `@Transient`

- 表示非数据库表的字段的映射, ORM框架将忽略该属性.
- 若不标识, ORM框架默认其注解为`@Basic`

### `@Temporal`

- 用于标识时间精度
- 备选属性：
  - `TemporalType.TIMESTAMP`
  - `TemporalType.DATE`
  - `TemporalType.TIME`

```JAVA
@Table(name="XXX_PERSONS")
@Entity
public class Person {
  @Id
  @GeneratedValue(strategy=GenerationType.IDENTITY)
  private long id;
  @Column(name="FIRST_NAME")
  private String firstName;
  @Column(name="LAST_NAME")
  private String lastName;
  @Column(length=50)
  private String email;
  @Temporal(TemporalType.DATE)
  private Date dateOfBirth;
  
  @Transient
  public String getFullName() {
    return firstName + " " + lastName;
  }
  
  // other getters and setters
  // ...
}
```

## 关系映射

### 双向多对一关系

```java
// Customer.java
@OneToMany(
  // fetch=FetchType.LAZY,
  // cascade={CascadeType.REMOVE},
  mappedBy="customer") // 由customer维持映射关系
private set<Order> orders;

// Order.java
@JoinColumn(name="CUSTOMER_ID") // 外键
@ManyToOne(
  // fetch=FetchType.LAZY
)
private Customer customer;
```

- `@ManyToOne`: 多对一关系
  - `fetch` 属性：加载策略
- `@JoinColumn`： 外键
  - `name`属性：此外键在此表中的名字



- `@OneToMany`: 一对多关系
  - `fetch`属性：加载策略
  - `cascade`属性：删除策略
  - `mappedBy`属性：由哪一方维护映射关系，通常选择“多”的一方



### 双向一对一关系

```java
// Manager.java
@OneToOne(mappedBy="mgr") // 不可以设置lazy
private Department department;

// Department.java
@JoinColumn(name="MGR_ID", unique=true) // 可设置lazy
@OneToOne
private Manager mgr;
```

### 双向多对多关系

两方中，有一方需要放弃维护关系

```java
// Item.java
@JoinTable(name="ITEM_CATEGORY",
           joinColumns={@JoinColumn(name="ITEM_ID", referencedColumnName="ID")},
           inverseJoinColumns={@JoinColumn(name="CATEGORY_ID", referencedColumnName="ID")})
@ManyToMany
private Set<Category> categories;

// Category.java
@ManyToMany(mappedBy="categories") // 由Item类中的categories维护映射关系
private Set<Item> items;
```

`jointable`

```java
@JoinTable(
  name="中间表的名字",
  joinColumns={
    @JoinColumn(name="当前表主键对应的外键的名字", 
                referencedColumnName="所对应的当前表的列名" // 可以不写
               )
  },
  inverseJoinColumns={
    @JoinColumn(name="另一个表主键对应的外键的名字", 
                referencedColumnName="所对应的另一个表的列名" //可以不写
               )
  }
)
```



