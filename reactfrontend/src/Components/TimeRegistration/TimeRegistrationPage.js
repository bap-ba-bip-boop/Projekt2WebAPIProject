import React, {useEffect, useState} from 'react'

import { getData } from '../Data/JSONData';
import appSettings from '../../Settings/Components/TimeRegistration/TimeRegistrationPage.json';


export const TimeRegistrationPage = props => {
  
    const url = appSettings.apiUrl + `/${props.currentRegId}`;
    const [currentReg, setCurrentReg] = useState( null );

    useEffect(()=>{
      getData(url).then( result => {
          setCurrentReg(result);
       }
      )
      },
      []
    );

    return (
        <section className='regPagePanel'>
        <div className='regPanelTitle'>
            <h2>{currentReg && currentReg.projectName}</h2>
        </div>

          <div className='regPanelLabel'>
              <label>Customer: </label>
              <p>{currentReg && currentReg.customerName}</p>
          </div>
          <div className='regPanelLabel'>
              <label>Date: </label>
              <p>{currentReg && currentReg.datum.split("T")[0]}</p>
          </div>
          <div className='regPanelLabel'>
              <label>Time: </label>
              <p>{currentReg && currentReg.antalMinuter} Minutes</p>
          </div>
          <div className='regPanelDescriptionContainer'>
              <label>Beskrivning:</label>
              <p className='regPanelDescription'>{currentReg && currentReg.beskrivning}</p>
          </div>
        </section>
    )
}
