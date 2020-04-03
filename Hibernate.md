# Hibernate

## What is Hibernate?

- 一个框架
- 一个Java领域的持久化框架
- 一个ORM框架

### 对象的持久化

- 狭义的理解：“持久化”指把数据永久保存到数据库中
- 广义的理解：“持久化“包括与数据库相关的各种操作（增删改查，加载）
  - 加载：根据特定的OID(Object Identifier)，把对象从数据库加载到内存中

### ORM

- Object Relation Mapping: 对象-关系 映射
- 把对数据库的操作转化为对对象的操作
- ORM 采用**元数据**来描述**对象-关系映射**细节
  - 元数据通常采用 XML 格式, 并且存放在专门的对象-关系映射文件中
  - 元数据：描述数据的数据



## Hibernate开发步骤

1. 创建Hibernate配置文件
2. 创建持久化类
3. 创建对象-关系映射文件
4. 通过Hibernate API编写访问数据库的代码

### 1. 创建Hibernate配置文件

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE hibernate-configuration PUBLIC
		"-//Hibernate/Hibernate Configuration DTD 3.0//EN"
		"http://hibernate.sourceforge.net/hibernate-configuration-3.0.dtd">
<hibernate-configuration>
	<session-factory>
    
		<!-- 配置连接数据库的基本信息 -->
		<property name="connection.username">root</property>
		<property name="connection.password">1230</property>
		<property name="connection.driver_class">com.mysql.jdbc.Driver</property>
		<property name="connection.url">jdbc:mysql:///hibernate5</property>
		
		<!-- 配置 hibernate 的基本信息 -->
		<!-- hibernate 所使用的数据库方言 -->
		<property name="dialect">org.hibernate.dialect.MySQLInnoDBDialect</property>		
		
		<!-- 执行操作时是否在控制台打印 SQL -->
		<property name="show_sql">true</property>
	
		<!-- 是否对 SQL 进行格式化 -->
		<property name="format_sql">true</property>
	
		<!-- 指定自动生成数据表的策略 -->
		<property name="hbm2ddl.auto">update</property>
		
		<!-- 指定关联的 .hbm.xml 文件 -->
		<mapping resource="com/atguigu/hibernate/helloworld/News.hbm.xml"/>
	
	</session-factory>

</hibernate-configuration>
```

### 2. 创建持久化Java类

- 提供一个无参的构造器
  - 使Hibernate可以使用Constructor.newInstance() 来实例化持久化类
  - 反射原理
- 提供一个标识属性（identifier property）
- get/set方法
- 非final类
- 重写equals和hashCode方法
  - 如果需要把持久化类的实例放到 Set 中(当需要进行关联映射时), 则应该重写这两个方法

### 3. 创建对象-关系映射文件

- Hibernate采用XML格式的文件来制定对象和关系数据之间的映射
- 在运行时Hibernate将根据这个映射文件来生成各种SQL语句
- 映射文件的扩张名为`.hbm.xml`

```xml
<?xml version="1.0"?>
<!DOCTYPE hibernate-mapping PUBLIC "-//Hibernate/Hibernate Mapping DTD 3.0//EN"
"http://hibernate.sourceforge.net/hibernate-mapping-3.0.dtd">

<hibernate-mapping package="com.atguigu.hibernate.helloworld">
    <class name="News" table="NEWS" dynamic-insert="true">
        
        <id name="id" type="java.lang.Integer">
            <column name="ID" />
            <!-- 指定主键的生成方式, native: 使用数据库本地方式 -->
            <generator class="native" />
        </id>
    
        <property name="title" type="java.lang.String" column="TITLE" >
        </property>
        
        <property name="author" type="java.lang.String">
            <column name="AUTHOR" />
        </property>
        
        <property name="date" type="date">
            <column name="DATE" />
        </property>		
    </class> 
</hibernate-mapping>
```

### 4. 通过Hibernate API访问数据库

```java
//1. 创建一个 SessionFactory 对象
SessionFactory sessionFactory = null;

//1). 创建 Configuration 对象: 对应 hibernate 的基本配置信息和 对象关系映射信息
Configuration configuration = new Configuration().configure();

//2). 创建一个 ServiceRegistry 对象: hibernate 4.x 新添加的对象
//hibernate 的任何配置和服务都需要在该对象中注册后才能有效.
ServiceRegistry serviceRegistry = 
    new ServiceRegistryBuilder().applySettings(configuration.getProperties())
    .buildServiceRegistry();

//3). 注入registry
sessionFactory = configuration.buildSessionFactory(serviceRegistry);

//2. 创建一个 Session 对象
Session session = sessionFactory.openSession();

//3. 开启事务
Transaction transaction = session.beginTransaction();

//4. 执行保存操作
News news = new News("Java12345", "ATGUIGU", new Date(new java.util.Date().getTime()));
session.save(news);

//5. 提交事务 
transaction.commit();

//6. 关闭 Session
session.close();

//7. 关闭 SessionFactory 对象
sessionFactory.close();
```



## Hibernate配置项

`hbm2ddl.auto`

取值：`create` | `update` | `create-drop` | `validate`

- create : 会根据 `.hbm.xml` 文件来生成数据表, 但是每次运行都会**删除上一次的表** ,重新生成表, 哪怕二次没有任何改变

- create-drop : 会根据 .hbm.xml 文件生成表,但是SessionFactory一关闭, 表就自动删除

- update : **最常用的属性值**，也会根据 .hbm.xml 文件生成表, 但若 .hbm.xml 文件和数据库中对应的数据表的表结构不同, Hiberante 将更新数据表结构，但不会删除已有的行和列

- validate : 会和数据库中的表进行比较, 若 .hbm.xml 文件中的列在数据表中不存在，则抛出异常

`format_sql`

是否将 SQL 转化为格式良好的 SQL . 取值 `true` | `false`



## 持久化对象的状态

Hibernate 把对象分为 4 种状态: **持久化状态, 临时状态, 游离状态, 删除状态**. Session 的特定方法能使对象从一个状态转换到另一个状态.

### 临时 - Transient

- 在使用代理主键的情况下, OID通常为null
- 不处于 Session 的缓存中
- 在数据库中没有对应的记录

### 持久化 - 托管 - Persist

- OID不为null
- 位于Session缓存中
- 若在数据库中已经有和其对应的记录, 持久化对象和数据库中的相关记录对应
- Session在flush缓存时,会根据持久化对象的属性变化,来同步更新数据库
- 在同一个Session实例的缓存中,数据库表中的每条记录只对应唯一的持久化对象

### 删除 - Removed

- 在数据库中没有和其 OID 对应的记录
- 不再处于 Session 缓存中
- 一般情况下, 应用程序不该再使用被删除的对象

### 游离 - 脱管 - Detached

- OID不为null
- 不再处于Session缓存中
- 一般情况需下, 游离对象是由持久化对象转变过来的, 因此在数据库中可能还存在与它对应的记录

### 状态转换

![](https://raw.githubusercontent.com/Nathan-Mu/img/master/notes/hibernate/object_status_transfer.png)



## 映射文件 `*.hbm.xml`

### 结构

- hibernate-mapping
  - 类层次：class
    - 键：id
    - 基本类型: property
    - 实体引用类: many-to-one | one-to-one
    - 集合: set | list | map | array
      - one-to-many
    	- many-to-many
    - 子类: subclass | joined-subclass
    - 其它: component | any 等
  - 查询语句:query（用来放置查询语句，便于对数据库查询的统一管理和优化

### Example

```xml
<?xml version="1.0"?>
<!DOCTYPE hibernate-mapping PUBLIC "-//Hibernate/Hibernate Mapping DTD 3.0//EN"
"http://hibernate.sourceforge.net/hibernate-mapping-3.0.dtd">

<hibernate-mapping package="com.atguigu.hibernate.strategy">

    <class name="Order" table="ORDERS">

        <id name="orderId" type="java.lang.Integer">
            <column name="ORDER_ID" />
            <generator class="native" />
        </id>
        
        <property name="orderName" type="java.lang.String">
            <column name="ORDER_NAME" />
        </property>
        
		<many-to-one 
			name="customer" class="Customer" 
			column="CUSTOMER_ID"
			lazy="false"
			fetch="join"></many-to-one>

    </class>
</hibernate-mapping>
```

### `hibernate-mapping`的属性

- `auto-import=true`
  - 默认为 true
  - 指定是否可以在查询语言中使用非全限定的类名（仅限于本映射文件中的类）
- `catelog`
  - 指定所映射的数据库catalog的名称
- `default-access="property"`
  - 默认为 property
  - 指定 Hibernate 的默认的属性访问策略
  - `property`: 使用 getter, setter 方法来访问属性
  - `access`: 忽略 getter/setter 方法, 通过反射访问成员变量
- `default-cascade="none"`
  - 默认为 none
  - 设置hibernate默认的级联风格
  - 若配置 Java 属性, 集合映射时没有指定 cascade 属性, 则 Hibernate 将采用此处指定的级联风格.   
- `default-lazy=true`
  - 默认为 true
  - 设置 Hibernate morning的延迟加载策略
  - `true`: 启用延迟加载策略
  - 若配置 Java 属性映射, 集合映射时没有指定 lazy 属性, 则 Hibernate 将采用此处指定的延迟加载策略
- `package`
  - 指定一个包前缀，如果在映射文档中没有指定全限定的类名，就使用这个作为包名
- `schema`
  - 指定所映射的数据库schema的名称
  - 若指定该属性, 则表明会自动添加该 schema 前缀

### `class`的属性

- `abstract="true"`
- `batch-size`
  - 指定根据 OID 来抓取实例时每批抓取的实例数
- `catalog`
- `check`
- `discriminator-value`
  - 指定区分不同子类的值. 当使用 `<subclass/> `元素来定义持久化类的继承关系时需要使用该属性
- `dynamic-insert="false"`
  - 默认值为 false
  - `true`: 当保存一个对象时, 动态生成 insert 语句, insert 语句中仅包含取值不为 null 的字段
- `dynamic-update="false"`
  - 默认值为 false
  - `true`: 当更新一个对象时, 动态生成 update 语句, update 语句中仅包含取值需要更新的字段
- `entity-name`
- `lazy="true"`
  - 指定是否使用延迟加载
- `mutable="true"`
	- 默认为 true
	- `true`: 等价于所有的 `<property> `元素的 update 属性为 false, 整个实例不能被更新. 
- `name`
  - 指定该持久化类映射的持久化类的类名
- `node`
- `optimistic-lock="version"`
- `persister`
- `polymorphism="implicit"`
- `proxy`
- `rowid`
- `schema`
- `select-before-update="false"`
  - 默认值为 false
  - 设置 Hibernate 在更新某个持久化对象之前是否需要先执行一次查询
- `subselect`
- `table`
  - 指定该持久化类映射的表名
  - Hibernate 默认以持久化类的类名作为表名
- `where`

### `id`的属性

- `access`
- `column`
  - 设置标识属性所映射的数据表的列名(主键字段的名字)
- `length`
- `name`
  - 标识持久化类 OID 的属性名
- `node`
- `type`
  - 指定 Hibernate 映射类型
  - 如果没有为某个属性显式设定映射类型, Hibernate 会运用反射机制先识别出持久化类的特定属性的 Java 类型, 然后自动使用与之对应的默认的 Hibernate 映射类型
- `unsaved-value`
  - 若设定了该属性, Hibernate 会通过比较持久化类的 OID 值和该属性值来区分当前持久化类的对象是否为临时对象

### `generator`的属性

- `class`: 指定使用的标识符生成器全限定类名或其缩写名

### `generator`的策略

![](https://raw.githubusercontent.com/Nathan-Mu/img/master/notes/hibernate/id_generator_strategies.png)

### `property`的属性

- `name`
  - 指定该持久化类的属性的名字
- `access`
  - 默认值为 `property`
  - 指定 Hibernate 的默认的属性访问策略
  - `true`: 使用 getter, setter 方法来访问属性
  - `field`: Hibernate 会忽略 getter/setter 方法, 而通过反射访问成员变量
- `column`
  - 指定与类的属性映射的表的字段名. 如果没有设置该属性, Hibernate 将直接使用类的属性名作为字段名
- `formula`
  - 设置一个 SQL 表达式, Hibernate 将根据它来计算出派生属性的值. 
  - 派生属性: 并不是持久化类的所有属性都直接和表的字段匹配, 持久化类的有些属性的值必须在运行时通过计算才能得出来, 这种属性称为派生属性
  - 使用 formula 属性时，formula=“(sql)” 的英文括号不能少
  - Sql 表达式中的列名和表名都应该和数据库对应, 而不是和持久化对象的属性对应
  - 如果需要在 formula 属性中使用参数, 可直接使用 where cur.id=id 形式, 其中 id 就是参数, 和当前持久化对象的 id 属性对应的列的 id 值将作为参数传入. 
- `generated="never"`
- `index`
  - 指定一个字符串的索引名称
  - 当系统需要 Hibernate 自动建表时, 用于为该属性所映射的数据列创建索引, 从而加快该数据列的查询.
- `insert="true"`
- `lazy="false"`
- `length`
  - 指定该属性所映射数据列的字段的长度
- `node`
- `not-null="false"`
  - 默认为 false
  - `true`: 不允许为 null
- `optimistic-lock="true"`
- `precision`
- `scale`
  - 指定该属性所映射数据列的小数位数, 对 double, float, decimal 等类型的数据列有效.
- `type`
  - 指定 Hibernate 映射类型
  -  如果没有为某个属性显式设定映射类型, Hibernate 会运用反射机制先识别出持久化类的特定属性的 Java 类型, 然后自动使用与之对应的默认的 Hibernate 映射类型.
- `unique-key`
- `unique="false"`
  - 设置是否为该属性所映射的数据列添加唯一约束
- `update="true"`
  - 设置是否可以更新属性值

### Java 类型, Hibernate 映射类型及 SQL 类型的对应关系

![](https://raw.githubusercontent.com/Nathan-Mu/img/master/notes/hibernate/relationship_between_java_hibernate%26sql_type1.png)

![](https://raw.githubusercontent.com/Nathan-Mu/img/master/notes/hibernate/relationship_between_java_hibernate%26sql_type2.png)

### Java 大对象类型的 Hiberante 映射

> ==保存图片，长文本==  *Hibernate - 10 23'00*

![](https://raw.githubusercontent.com/Nathan-Mu/img/master/notes/hibernate/java_big_type_in_hibernate.png)

- `java.lang.String` 可用于表示长字符串(长度超过 255)
- 字节数组 `byte[] `可用于存放图片或文件的二进制数据
- JDBC API 提供了 `java.sql.Clob `和 `java.sql.Blob` 类型, 它们分别和标准 SQL 中的 `CLOB` 和 `BLOB` 类型对应.
- CLOB 表示字符串大对象(Character Large Object)
- BLOB表示二进制对象(Binary Large Object)
- 在持久化类中, 二进制大对象可以声明为 `byte[]` 或 `java.sql.Blob` 类型; 
- 字符串可以声明为 `java.lang.String` 或 `java.sql.Clob`

### Cascade

![](https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/hibernate/hibernate_cascade.png)