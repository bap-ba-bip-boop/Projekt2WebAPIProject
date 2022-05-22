import React, { useState, useEffect } from 'react';

import {Header} from './Components/Header'
import {Main} from './Components/Main'
import {Footer} from './Components/Footer'
import {ButtonBar} from './Components/ButtonBar';
import { getData } from './Components/Data/JSONData';
import'./Style/style.css';

String.prototype.format = function () {
  // store arguments in an array
  var args = arguments;
  // use replace to iterate over the string
  // select the match and check if related argument is present
  // if yes, replace the match with the argument
  return this.replace(/{([0-9]+)}/g, function (match, index) {
    // check if the argument is present
    return typeof args[index] == 'undefined' ? match : args[index];
  });
};

const appSettingslocation = './Settings/App.json';

const App = () => {
  const [appSettings, setAppSettings] = useState([]);

  useEffect(()=>{
    getData(appSettingslocation).then( result => {
      setAppSettings(result);
    }
    )
    },
    []
  );

  const [activePage, setActivePage] = useState(appSettings.startPage);
  const [currentRegId, setRegId] = useState(0);
  const [url, setCurrentUrl] = useState("");

  const changeActivePage = newPage => {
    setActivePage(newPage);
  }
  const setCurrentTimeReg = (newId) => {
    setRegId(newId);
    setCurrentUrl(appSettings.apiUrl + `/${currentRegId}`)
  }

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
          changeActivePage(appSettings.startPage)
        }
      )
  }

  return(
    <div className='siteContainer'>
    <Header 
      changeActivePage={changeActivePage}
      startPage={appSettings.startPage}
      />
    <ButtonBar 
      activePage={activePage} 
      changeActivePage={changeActivePage} 
      deleteSelectedReg={deleteSelectedReg}
      editPage={appSettings.editPage}
      newPage={appSettings.newPage} 
      regPage={appSettings.regPage} 
      startPage={appSettings.startPage} 
      />
    <Main 
      activePage={activePage} 
      changeActivePage={changeActivePage}
      currentRegId={currentRegId}
      editPage={appSettings.editPage}
      newPage={appSettings.newPage}
      regPage={appSettings.regPage}
      setCurrentTimeReg={setCurrentTimeReg}
      startPage={appSettings.startPage}
      />
    <Footer/>
  </div>
  )
}

export default App;
