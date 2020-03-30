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