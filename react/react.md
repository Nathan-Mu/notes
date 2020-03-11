# React

## 1. Basics

### Function components

```react
// Person.js
import React from 'react';
const person = (props) => {
    return (
        <div>
            <p>I'm {props.name} and I am {props.age} years old!</p>
            <div>{props.children}</div>
        </div>
    )
}
```

### React Hooks - useState()

```react
import React, {useState} from 'react';

const app = props => {
    // useState() will always return an array with two elements.
    // The first one is the current state object
    // The second one is the method to **REPLACE** the previous state
    const [personsState, setPersonsState] = useState({
        persons: [
            {name: 'Max', age: 18},
            {name: 'Sue', age: 19},
            {name: 'Mike', age: 23},
        ],
        secondState: 'second state'
    });
    
    // for useState() method, you don't need to hold one single large state object, you can have multiple state by using useState() method
    const [thirdState, setThirdState] = useState('third state');
    
    const handleClick = () => {
        setPersonsState({
            persons: [
                {name: 'Maxxxxx', age: 18},
                {name: 'Sueeeee', age: 19},
                {name: 'Mikeeeee', age: 23},
            ]
        }); // secondState is lost here
    };
    
    return (
    	<div>
            <button onClick={handleClick}>Switch</button>
            <p>Name: {personsState.persons[0].name}, 
                age: {personsState.persons[0].age}</p>
            <p>Name: {personsState.persons[1].name}, 
                age: {personsState.persons[1].age}</p>
            <p>Name: {personsState.persons[2].name}, 
                age: {personsState.persons[2].age}</p>
        </div>
    )
}

export default app;
```

### Stateful & stateless components

### 2 ways of passing parameter (bind & anonymous function)

```react
class App extends Component {
    state = {
        name: 'Max'
    };
    
    const handleClick = (newName) => {
        this.setState({
            name: newName
        });
    };

	render() {
        return (
            <div>
            	<div>{this.state.name}</div>
                <!-- 1.bind (efficient) -->
                <button onClick={this.handleClick.bind(this, 'Tom')}>Switch1</button>
                <!-- 2.anonymous function (INEFFICIENT) -->
                <button onClick={() => this.handleClick('Tom')}>Switch1</button>
            </div>
        ) 
    }
}
```

 

### copy an array

1. `const numbers = this.state.numbers.slice()`
2. `const numbers = [...this.state.numbers]`



## 2. React Lifecycle

### Side Effect

A "side effect" is anything that **affects something outside the scope of the function being executed**. These can be, say, **a network request**, which has your code communicating with a third party (and thus making the request, causing logs to be recorded, caches to be saved or updated, all sorts of effects that are outside the function.

There are more subtle side effects, too. Changing the value of a closure-scoped variable is a side effect. Pushing a new item onto an array that was passed in as an argument is a side effect. Functions that execute without side effects are called "pure" functions: they take in arguments, and they return values. Nothing else happens upon executing the function. This makes the easy to test, simple to reason about, and functions that meet this description have all sorts of useful properties when it comes to optimization or refactoring.

Pure functions are deterministic (meaning that, given an input, they always return the same output), but that doesn't mean that all impure functions have side effects. Generating a random value within a function makes it impure, but isn't a side effect, for example. React is all about pure functions, and asks that you keep several lifecycle methods pure, so these are good questions to be asking.

### React Common Lifecycles

http://projects.wojtekmaj.pl/react-lifecycle-methods-diagram/

![](F:\notes\react\pics\react_common_lifecycles.png)

### React Lifecycles

![](F:\notes\react\pics\react_whole_lifecycles.png)

### Component Lifecycle - Creation

![](F:\notes\react\pics\Component Lifecycle - Creation.png)

### Component Lifecycle - Update

![](F:\notes\react\pics\Component Lifecycle - Update.png)

### static getDerivedStateFromProps()

This method will return a updated state. For example:

```react
static getDerivedStateFromProps(props, state) {
    return state; // return a state object and it should be updated in a real application
}
```

In this method, normally we sync state from new props. However, it method would rarely be called in a real application. 

### getSnapshotBeforeUpdate() & componentDidUpdate() 

```react
getSnapshotBeforeUpdate(prevProps, prevState) {
    return { message: 'Snapshot'}
}

componentDidUpdate(prevProps, prevState, snapshot) {
    console.log(snapshot);
}
```

## 3. es6 refresher

### let & const

`var`: function scope

`let`: block scope

`const`: block scope, cannot be reassigned a value

### Deconstructing

```react
const address = {
    street: '',
    city: '',
    country: ''
}
const {street, city, country} = address;
const {street:st} = address;
```

