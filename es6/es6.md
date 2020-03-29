# ES6

## 1. let & const

let : variable values

const: constant values

## 2. Arrow functions

es5: 

```js
function myFunc() {
    ...
}
```

es6: 

```javascript
const myFunc = () => {
    ...
}
```

Benefits: no more issues with the `this` keyword

Example: 

```js
const printMyName = name => {
    console.log(name);
}

const printMyFullName = (fn, ln) => {
    console.log(fn + ln);
}

const getMyFullName = (fn, ln) => fn + ln;
```

## 3. Exports & Imports

### 2 ways of export

```js
// person.js
const person = {
    name: 'Max'
}
export default person;
```

```js
// utility.js
export const clean = () =>{...}
export const baseData = 10;
```

### 2 ways of import 

```js
// default import
import person from './person.js';
import p from './person.js';

// named import
import {baseData, clean} from './utility.js';
import {clean as c} from './utility.js';
import * as bundled from './utility.js';
```

## 4. Classes

es6:

```js
constructor() {
    this.myProperty = 'value';
}

myMethod() {...}
```

es7: 

```js
myProperty = 'value';
myMethod = () => {...}
```

Example: 

```js
class Human {
    gender = 'male';

	printGender = () =>{
        console.log(this.gender);
    }
}

class Person extends Human {
    name = 'Max';
	gender = 'female';

	printMyName = () => {
        console.log(this.name);
    }
}

const person = new Person();
person.printMyName();
person.printGender();

// results:
// "Max"
// "female"
```

## 5. Spread & Rest Operators

### Spread

Used to split up array elements OR object properties

```js
const newArray = [...oldArray, 1, 2]
const newObject = {...oldObject, newProp: 65}
```

### Rest

Used to merge a list of function arguments into an array

```js
function sortArgs(...args) {...}
```

## 6. Destructuring

Easily extract array elements or object properties and store them in variables. 

### Array Destructuring

```js
[a, b] = ['Hello', 'Max'];
[n, , m] = [1, 2, 3]; 
```

### Object Destructuring

```js
{age} = {name: 'Max', age: 28};
```

## 7. Array functions

### map()

```js
const numbers = [1, 2, 3];
const doubleNumberArray = numbers.map((num) => {
    return num * 2;
})
```



































