import React, { useState, useEffect } from 'react'

import { ErrorMessage } from '../ErrorMessage';
import {getData} from '../Data/JSONData';
import appSettings from '../../Settings/Components/TimeRegistration/TimeRegistrationEdit.json';
import { minuteValidation, dateValidation, descValidation } from '../SharedMethods/FormValidation';

export const TimeRegistrationEdit = props => {
  const itemUrl = props.getOneRegUrl + `/${props.currentRegId}`;

  const [timeReg, setTimeReg] = useState([]);

  useEffect(()=>{
    getData(itemUrl)
    .then( result => {
      setTimeReg(result)
      SetDatum(result.datum.split("T")[0]);
      SetAntalMinuter(result.antalMinuter);
      SetBeskrivning(result.beskrivning);
     })
    },
    []
  );
  
  const [Datum, SetDatum] = useState(timeReg.datum);
  const [AntalMinuter, SetAntalMinuter] = useState(timeReg.antalMinuter);
  const [Beskrivning, SetBeskrivning] = useState(timeReg.beskrivning);

  const [minuteError, setMinuteError] = useState("");
  const [dateError, setDateError] = useState("");
  const [descError, setDescError] = useState("");

  const onRegister = (event)=>{
    let encoutneredErrors = false;
    event.preventDefault();

    let minuteVal = minuteValidation(AntalMinuter);
    if(minuteVal != "")
    {
      setMinuteError(minuteVal);
      encoutneredErrors = true;
    }

    let dateVal = dateValidation(Datum);
    if(dateVal != "")
    {
      setDateError(dateVal);
      encoutneredErrors = true;
    }

    let descVal = descValidation(Beskrivning);
    if(descVal != "")
    {
      setDescError(Beskrivning);
      encoutneredErrors = true;
    }

    if(!encoutneredErrors)
    {
      appSettings.PutDTO.Beskrivning = Beskrivning;
      appSettings.PutDTO.Datum = Datum;
      appSettings.PutDTO.AntalMinuter = AntalMinuter;
      fetch(
        itemUrl,
        {
          method: appSettings.fetchMethod,
          headers: appSettings.fetchHeaders,
          body: JSON.stringify(appSettings.PutDTO)
        }
      ).then(
        result =>
        {
          //console.log(result);
          props.changeActivePage(props.startPage)
        }
      )
    }
  }

  return (
    <form>
      <div className='formGroup'>
        <label className='formLabel'>Antal Minuter</label>
        <input
        className='formInput'
        onChange={e=>SetAntalMinuter(e.target.value)}
        max={appSettings.maxMinutes}
        min={appSettings.minMinutes}
        required
        type="number"
        value={AntalMinuter}
        />
        {minuteError !== "" && <ErrorMessage message={minuteError}/>}
      </div>
      
      <div className='formGroup'>
        <label className='formLabel'>Datum</label>
        <input
        className='formInput'
        onChange={e=>SetDatum(e.target.value)}
        required
        type="date"
        value={Datum}
        />
        {dateError !== "" && <ErrorMessage message={dateError}/>}
      </div>

      <div className='formGroup'>
        <label className='formLabel'>Beskrivning</label>
        <textarea
        className='formTextArea'
        onChange={e=>SetBeskrivning(e.target.value) }
        maxLength={appSettings.maxStrLength}
        rows="4"
        value={Beskrivning}
        ></textarea>
        {descError !== "" && <ErrorMessage message={descError}/>}
      </div>

      <input className='formSubmit' onClick={(e)=>onRegister(e)} type="submit" value="Edit"/>
    </form>
  )
}