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

- 在使用代理主键的情况下, **OID通常为null**
- **不处于 Session 的缓存中**
- **在数据库中没有对应的记录**

### 持久化 - 托管 - Persist

- **OID不为null**
- **位于Session缓存中**
- 若在数据库中已经有和其对应的记录, **持久化对象和数据库中的相关记录对应**
- **Session在flush缓存时,会根据持久化对象的属性变化,来同步更新数据库**
- **在同一个Session实例的缓存中,数据库表中的每条记录只对应唯一的持久化对象**

### 删除 - Removed

- 在数据库中没有和其 OID 对应的记录
- 不再处于 Session 缓存中
- 一般情况下, 应用程序不该再使用被删除的对象

### 游离 - 脱管 - Detached

- **OID不为null**
- **不再处于Session缓存中**
- 一般情况需下, 游离对象是由持久化对象转变过来的, 因此在数据库中可能还存在与它对应的记录

### 状态转换

![](https://raw.githubusercontent.com/Nathan-Mu/notes/master/.img/hibernate/object_status_transfer.png)