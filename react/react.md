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

 

### cope an array

1. `const numbers = this.state.numbers.slice()`
2. `const numbers = [...this.state.numbers]`

