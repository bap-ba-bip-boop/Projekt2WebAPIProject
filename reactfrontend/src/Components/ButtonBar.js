import React from 'react'
import { SiteButton } from './SiteButton'

export const ButtonBar = props => {

    const newtext = 'New';
    const backText = 'Back';
    const editText = 'Edit';
    const deleteText = 'Delete';
  return (
    <div className='buttonBar'>
        {props.activePage !== props.newPage && <SiteButton buttonText={newtext} buttonAction={props.changeActivePage} redirectPage={props.newPage}/>}
        {props.activePage === props.regPage && <SiteButton buttonText={editText} buttonAction={props.changeActivePage} redirectPage={props.editPage}/>}
        {props.activePage === props.regPage && <SiteButton buttonText={deleteText} buttonAction={props.deleteSelectedReg}/>}
        {props.activePage !== props.startPage && <SiteButton buttonText={backText} buttonAction={props.changeActivePage} redirectPage={props.startPage}/>}
    </div>
  )
}
