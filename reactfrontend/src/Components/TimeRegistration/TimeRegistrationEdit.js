import React, { useState, useEffect } from 'react'

import { ErrorMessage } from '../ErrorMessage';
import {getData} from '../Data/JSONData';

export const TimeRegistrationEdit = props => {
  const [appSettings, setAppSettings] = useState([]);

  useEffect(()=>{
    getData(props.settingsAddress).then( result => {
      setAppSettings(result);
    }
    )
    },
    []
  );

  const [itemUrl, setItemUrl] = useState(props.getOneRegUrl + `/${props.currentRegId}`);//appSettings.apiUrl +`/${props.currentRegId}`;

  const [timeReg, setTimeReg] = useState([]);

  useEffect(()=>{
    getData(itemUrl).then( result => {
      setTimeReg(result)
      SetDatum(result.datum.split("T")[0]);
      SetAntalMinuter(result.antalMinuter);
      SetBeskrivning(result.beskrivning);
     }
    )
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

    event.preventDefault();

    if(editForm.checkValidity())
    {
      appSettings.PutDTO.Beskrivning = Beskrivning;
      appSettings.PutDTO.Datum = Datum;
      appSettings.PutDTO.AntalMinuter = AntalMinuter;
      fetch(
        url,
        {
          method: appSettings.fetchMethod,
          headers: appSettings.fetchHeaders,
          body: JSON.stringify(appSettings.PutDTO)
        }
      ).then(
        result =>
        {
          console.log(result);
          props.changeActivePage(props.startPage)
        }
      )
    }
    else
    {
      console.log("form failed");
      console.log(AntalMinuter);
      if(!AntalMinuter || AntalMinuter === 0)
      {
        setMinuteError(appSettings.errMissingMinutes);
      }
      else if(AntalMinuter < appSettings.minMinutes)
      {
        setMinuteError(appSettings.errLessThanAllowedMin.format(appSettings.minMinutes));
      }
      else if(AntalMinuter > appSettings.maxMinutes)
      {
        setMinuteError(appSettings.errMoreThanAllowedMax.format(appSettings.maxMinutes));
      }
      else
      {
        setMinuteError("");
      }

      console.log(Datum);
      if(!Datum)
      {
        setDateError(appSettings.errDateMissing);
      }
      else
      {
        setDateError("");
      }

      console.log(Beskrivning.length);
      if(Beskrivning.length > appSettings.maxStrLength)
      {
        setDescError(appSettings.errDescTooLong.format(appSettings.maxStrLength));
      }
      else
      {
        setDescError("");
      }
    }
  }

  return (
    <section>
      <form id='editForm'>
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
    </section>
  )
}