import React from 'react'

import {TimeRegistrationIndex} from './TimeRegistration/TimeRegistrationIndex';
import {TimeRegistrationEdit} from './TimeRegistration/TimeRegistrationEdit';
import {TimeRegistrationNew} from './TimeRegistration/TimeRegistrationNew';
import {TimeRegistrationPage} from './TimeRegistration/TimeRegistrationPage';

export const Main = props => {

  const tidsRegistreringsAPIGet = "https://localhost:7045/tidsregistrering";
  const projectAPIGetAll = "https://localhost:7045/project";
  
  const newSettingsAddress = './Settings/Components/TimeRegistration/TimeRegistrationNew.json';
  const editSettingsAddress = './Settings/Components/TimeRegistration/TimeRegistrationEdit.json';
  const pageSettingsAddress = './Settings/Components/TimeRegistration/TimeRegistrationPage.json';
  
  return (
    <main className='siteMain'>
      {props.activePage === props.startPage && <TimeRegistrationIndex
        setCurrentTimeReg={props.setCurrentTimeReg}
        changeActivePage={props.changeActivePage}
        getAllRegUrl={tidsRegistreringsAPIGet}
        redirectPage={props.regPage}
        />}
      {props.activePage === props.newPage && <TimeRegistrationNew
        setCurrentTimeReg={props.setCurrentTimeReg}
        changeActivePage={props.changeActivePage}
        startPage={props.startPage}
        settingsAddress={newSettingsAddress}
        getAllProjUrl={projectAPIGetAll}
        />}
      {props.activePage === props.editPage && <TimeRegistrationEdit
        changeActivePage={props.changeActivePage}
        currentRegId={props.currentRegId}
        startPage={props.startPage}
        settingsAddress={editSettingsAddress}
        getOneRegUrl={tidsRegistreringsAPIGet}
        />}
      {props.activePage === props.regPage && <TimeRegistrationPage 
        currentRegId={props.currentRegId}
        settingsAddress={pageSettingsAddress}
        />}
    </main>
  )
}
