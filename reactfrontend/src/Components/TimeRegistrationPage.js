import React, {useEffect, useState} from 'react'
import { fetchOneReg } from './Data/OneRegData';


export const TimeRegistrationPage = props => {
    const url = `https://localhost:7045/tidsregistrering/${props.currentRegId}`;

    const [currentReg, setCurrentReg] = useState( null );

    useEffect(()=>{
        fetchOneReg(props.currentRegId).then( result => {
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
