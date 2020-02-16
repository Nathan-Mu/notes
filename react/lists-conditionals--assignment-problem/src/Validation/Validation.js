import React from 'react'

const validation = (props) => {
  return (
      <p>{props.length <= 5 ? 'Text too short' : 'Text long enough'}</p>
  )
}

export default validation