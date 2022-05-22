import React from 'react'

export const ErrorMessage = props => {
  return (
    <span className='formError'>{props.message}</span>
  )
}
