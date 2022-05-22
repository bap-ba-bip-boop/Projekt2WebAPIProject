import React from 'react'

export const SiteButton = props => 
  <a className='redirectButton' href="#" onClick={()=>props.buttonAction(props.redirectPage)}>{props.buttonText}</a>
