import React, { useState } from 'react';

import {Header} from './Components/Header'
import {Main} from './Components/Main'
import {Footer} from './Components/Footer'
import { ButtonBar } from './Components/ButtonBar';
import './Style/style.css'

const App = () => {

  const startPage = 'Start';
  const newPage = 'newReg';
  const regPage = 'regPage';
  const editPage = 'editPage';

  const [activePage, setActivePage] = useState(startPage);
  const [currentRegId, setRegId] = useState(0);

  const changeActivePage = newPage => {
    setActivePage(newPage);
  }

  const setCurrentTimeReg = (newId) => {
    setRegId(newId);
    setActivePage(regPage);
  }

  const url = `https://localhost:7045/tidsregistrering/${currentRegId}`;

  const deleteSelectedReg = (garbage) => {
    fetch(
      url,
      {
        method: "DELETE",
        headers: {
          'Content-Type': 'application/json'
        }
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
