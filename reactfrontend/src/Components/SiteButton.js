import React from 'react'

export const SiteButton = props => {
  return (
    <a className='redirectButton' href="#" onClick={()=>props.buttonAction(props.redirectPage)}>{props.buttonText}</a>
  )
}
