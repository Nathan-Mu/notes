# Spring Data

## @Query 注解

### SELECT

#### 使用JPQL

**如何传递参数？**

1. 使用占位符

```java
@Query("SELECT p FROM Person p WHERE p.lastName = ?1 AND p.email = ?2")
List<Person> getPersonByLastNameAndEmail(String lastName, String email);
```

2. 命名参数

```java
@Query("SELECT p FROM Person p WHERE p.lastName = :lastName AND p.email = :email")
List<Person> getPersonByLastNameAndEmail(@Param("lastName") String lastName, @Param("email") String email);
```

#### 使用原生sql语句

```java
@Query(value-"SELECT count(id) FROM jpa_persons", nativeQuery=true)
long getTotalCount();
```

### UPDATE & DELETE

```JAVA
@Modifying
@Query("UPDATE Person p SET p.email = :email WHERE id = :id")
void updatePersonEmail(@Param("id") Integer id, @Param("email") String email);
```

使用时需要声明事务

```java
// PersonService.java
@Transactional
public void updatePersonEmail(Integer id, String email) {
  personRepository.updatePersonEmail(id, email);
}
```



[至video 07]

