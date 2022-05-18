import React from 'react'
import { SiteButton } from './SiteButton'

export const ButtonBar = props => {

    const newtext = 'New Registration';
    const newPage = 'newReg'
  return (
    <div className='buttonBar'>
        {props.activePage !== newPage && <SiteButton buttonText={newtext} redirectPage={newPage} changeActivePage={props.changeActivePage}/>}
        {props.activePage !== 'Start' && <SiteButton buttonText={'Back'} redirectPage={'Start'} changeActivePage={props.changeActivePage}/>}
    </div>
  )
}
