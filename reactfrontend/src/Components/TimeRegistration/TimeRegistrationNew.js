import React, { useState, useEffect } from 'react'

import { ErrorMessage } from '../ErrorMessage';
import {getData} from '../Data/JSONData';
import appSettings from '../../Settings/Components/TimeRegistration/TimeRegistrationNew.json';
import { minuteValidation, dateValidation, projValidation, descValidation } from '../SharedMethods/FormValidation';

export const TimeRegistrationNew = props => {

  const [projects, setProjects] = useState([]);

  const [SelectedProject, setProjectId] = useState(0);
  const [Date, setDate] = useState(null);
  const [AntalMinuter, SetAntalMinuter] = useState(0);
  const [Beskrivning, setBeskrivning] = useState(0);

  const [minuteError, setMinuteError] = useState("");
  const [dateError, setDateError] = useState("");
  const [descError, setDescError] = useState("");
  const [projectError, setProjError] = useState("");

  useEffect(()=>{
    getData(props.getAllProjUrl)
    .then( result => {
      setProjects(result);
     })
    },
    []
  );

  const onRegister = (event) => {
    let encoutneredErrors = false;
    event.preventDefault();

    let minuteVal = minuteValidation(AntalMinuter);
    if(minuteVal != appSettings.validMessage)
    {
      setMinuteError(minuteVal);
      encoutneredErrors = true;
    }

    let dateVal = dateValidation(Date);
    if(dateVal != appSettings.validMessage)
    {
      setDateError(dateVal);
      encoutneredErrors = true;
    }

    let projVal = projValidation(SelectedProject);
    if(projVal != appSettings.validMessage)
    {
      setProjError(projVal);
      encoutneredErrors = true;
    }

    let descVal = descValidation(Beskrivning);
    if(descVal != appSettings.validMessage)
    {
      setDescError(Beskrivning);
      encoutneredErrors = true;
    }

    if(!encoutneredErrors)
    {
      appSettings.PostDTO.Beskrivning = Beskrivning;
      appSettings.PostDTO.Datum = Date;
      appSettings.PostDTO.AntalMinuter = AntalMinuter;
      appSettings.PostDTO.ProjectId = SelectedProject;
      fetch(
        props.getRegUrl,
        {
          method: appSettings.fetchMethod,
          headers: appSettings.fetchHeaders,
          body: JSON.stringify(appSettings.PostDTO)
        }
      )
      .then(
        result => {
          props.changeActivePage(props.startPage);
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
        max={appSettings.maxMinutes}
        min={appSettings.minMinutes}
        onChange={e=>SetAntalMinuter(e.target.value)}
        required
        type="number"
        />
        {minuteError !== "" && <ErrorMessage message={minuteError}/>}
      </div>
      
      <div className='formGroup'>
        <label className='formLabel'>Datum</label>
        <input
        className='formInput'
        onChange={e=>setDate(e.target.value)}
        required
        type="date"
        />
        {dateError !== "" && <ErrorMessage message={dateError}/>}
      </div>
      <div className='formGroup'>
        <label className='formLabel'>Project</label>
        <select defaultValue={""} className='formInput' onChange={e=>setProjectId(e.target.value)} required>
          <option disabled={true} value={""}>Välj ett Projekt</option>
          {projects.map( proj=>
            <option key={proj.projectId} value={proj.projectId}>{proj.projectName}</option>
          )}
        </select>
        {projectError !== "" && <ErrorMessage message={projectError}/>}
      </div>
      <div className='formGroup'>
        <label className='formLabel'>Beskrivning</label>
        <textarea
        className='formTextArea'
        onChange={e=>setBeskrivning(e.target.value) }
        rows="4"
        maxLength={appSettings.maxStrLength}
        >
        </textarea>
        {descError !== "" && <ErrorMessage message={descError}/>}
      </div>
      <input className='formSubmit' onClick={e=>{
        onRegister(e);
        }
        } type="submit" value="Create"/>
    </form>
  )
}
