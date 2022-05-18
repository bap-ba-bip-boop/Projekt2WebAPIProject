import React, { useState, useEffect } from 'react';

import {Header} from './Components/Header'
import {Main} from './Components/Main'
import {Footer} from './Components/Footer'
import { ButtonBar } from './Components/ButtonBar';
import './Style/style.css'

const App = () => {

  const startPage = 'Start';
  const [activePage, setActivePage] = useState(startPage);

  const changeActivePage = newPage => {
    setActivePage(newPage);
  }

  return(
    <div className='siteContainer'>
    <Header changeActivePage={changeActivePage} startPage={startPage}/>
    <ButtonBar changeActivePage={changeActivePage} activePage={activePage}/>
    <Main changeActivePage={changeActivePage} startPage={startPage} activePage={activePage}/>
    <Footer/>
  </div>
  )
}

export default App;
