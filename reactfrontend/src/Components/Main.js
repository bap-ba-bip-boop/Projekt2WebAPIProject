import React from 'react'

import {TimeRegistrationIndex} from './TimeRegistrationIndex';
import {TimeRegistrationEdit} from './TimeRegistrationEdit';
import {TimeRegistrationNew} from './TimeRegistrationNew';
import {TimeRegistrationPage} from './TimeRegistrationPage';

export const Main = props => {

    return (
      <main className='siteMain'>
        {props.activePage === props.startPage && <TimeRegistrationIndex setCurrentTimeReg={props.setCurrentTimeReg}/>}
        {props.activePage === props.editPage && <TimeRegistrationEdit changeActivePage={props.changeActivePage} currentRegId={props.currentRegId} startPage={props.startPage}/>}
        {props.activePage === props.newPage && <TimeRegistrationNew setCurrentTimeReg={props.setCurrentTimeReg} changeActivePage={props.changeActivePage} startPage={props.startPage}/>}
        {props.activePage === props.regPage && <TimeRegistrationPage currentRegId={props.currentRegId}/>}
      </main>
    )
}
