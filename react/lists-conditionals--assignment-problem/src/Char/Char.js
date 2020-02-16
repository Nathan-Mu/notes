import React from "react"

const char = (props) => {
  return (
      <div style={{display: 'inline-block', padding: '16px', textAlign: 'center', margin: '16px', border: '1px solid black'}}
        onClick={() => props.handleClick(props.char)}>
        {props.char}
      </div>
  )
}

export default char;