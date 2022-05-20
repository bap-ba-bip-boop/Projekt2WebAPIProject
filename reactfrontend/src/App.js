import React, { useState } from 'react';

import {Header} from './Components/Header'
import {Main} from './Components/Main'
import {Footer} from './Components/Footer'
import {ButtonBar} from './Components/ButtonBar';
import'./Style/style.css';


const App = () => {
  const appSettings = require('./Settings/App.json');

  const startPage = appSettings.startPage;
  const newPage = appSettings.newPage;
  const regPage = appSettings.regPage;
  const editPage = appSettings.editPage;

  const [activePage, setActivePage] = useState(startPage);
  const [currentRegId, setRegId] = useState(0);

  const changeActivePage = newPage => {
    setActivePage(newPage);
  }

  const setCurrentTimeReg = (newId) => {
    setRegId(newId);
    setActivePage(regPage);
  }

  const url = appSettings.apiUrl + `/${currentRegId}`;

  const deleteSelectedReg = (garbage) => {
    fetch(
      url,
      {
        method: appSettings.fetchMethod,
        headers: appSettings.fetchHeaders
      }
      ).then(
        result =>
        {
          console.log(result);
          changeActivePage(startPage)
        }
      )
  }

  return(
    <div className='siteContainer'>
    <Header 
      changeActivePage={changeActivePage}
      startPage={startPage}
      />
    <ButtonBar 
      activePage={activePage} 
      changeActivePage={changeActivePage} 
      deleteSelectedReg={deleteSelectedReg}
      editPage={editPage}
      newPage={newPage} 
      regPage={regPage} 
      startPage={startPage} 
      />
    <Main 
      activePage={activePage} 
      changeActivePage={changeActivePage}
      currentRegId={currentRegId}
      editPage={editPage}
      newPage={newPage}
      regPage={regPage}
      setCurrentTimeReg={setCurrentTimeReg}
      startPage={startPage}
      />
    <Footer/>
  </div>
  )
}

export default App;
