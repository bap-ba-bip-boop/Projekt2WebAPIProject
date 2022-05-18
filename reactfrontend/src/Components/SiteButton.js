import React from 'react'

export const SiteButton = props => {
  return (
    <a className='redirectButton' href="#" onClick={()=>props.changeActivePage(props.redirectPage)}>{props.buttonText}</a>
  )
}
