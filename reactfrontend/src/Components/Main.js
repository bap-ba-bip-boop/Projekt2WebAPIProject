import React from 'react'

import {TimeRegistrationIndex} from './TimeRegistration/TimeRegistrationIndex';
import {TimeRegistrationEdit} from './TimeRegistration/TimeRegistrationEdit';
import {TimeRegistrationNew} from './TimeRegistration/TimeRegistrationNew';
import {TimeRegistrationPage} from './TimeRegistration/TimeRegistrationPage';

//const tidsRegistreringsAPIGet = "https://localhost:7045/tidsregistrering";
//const projectAPIGetAll = "https://localhost:7045/project";
  

export const Main = props => 
    <main className='siteMain'>
      {props.activePage === props.startPage && <TimeRegistrationIndex
        changeActivePage={props.changeActivePage}
        redirectPage={props.regPage}
        setCurrentTimeReg={props.setCurrentTimeReg}
        getAllRegUrl={props.tidsRegistreringsAPIGet}
        />}
      {props.activePage === props.newPage && <TimeRegistrationNew
        changeActivePage={props.changeActivePage}
        setCurrentTimeReg={props.setCurrentTimeReg}
        startPage={props.startPage}
        getAllProjUrl={props.projectAPIGetAll}
        getRegUrl={props.tidsRegistreringsAPIGet}
        />}
      {props.activePage === props.editPage && <TimeRegistrationEdit
        changeActivePage={props.changeActivePage}
        currentRegId={props.currentRegId}
        startPage={props.startPage}
        getOneRegUrl={props.tidsRegistreringsAPIGet}
        />}
      {props.activePage === props.regPage && <TimeRegistrationPage 
        currentRegId={props.currentRegId}
        />}
    </main>
