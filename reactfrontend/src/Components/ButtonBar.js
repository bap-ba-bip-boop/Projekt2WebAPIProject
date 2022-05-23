import React from 'react'
import { SiteButton } from './SiteButton'
import appSettings from '../Settings/Components/ButtonBar.json';

export const ButtonBar = props => 
    <nav className='buttonBar'>
        {props.activePage !== props.newPage && <SiteButton buttonText={appSettings.NewButtonText} buttonAction={props.changeActivePage} redirectPage={props.newPage}/>}
        {props.activePage === props.regPage && <SiteButton buttonText={appSettings.EditButtonText} buttonAction={props.changeActivePage} redirectPage={props.editPage}/>}
        {props.activePage === props.regPage && <SiteButton buttonText={appSettings.DeleteButtonText} buttonAction={props.deleteSelectedReg}/>}
        {props.activePage !== props.startPage && <SiteButton buttonText={appSettings.BackButtonText} buttonAction={props.changeActivePage} redirectPage={props.startPage}/>}
    </nav>
