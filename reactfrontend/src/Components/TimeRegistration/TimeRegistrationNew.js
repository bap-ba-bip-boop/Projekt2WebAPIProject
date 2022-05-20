import React, { useState, useEffect } from 'react'

import {fetchAllProjects} from '../Data/AllProjectData';

export const TimeRegistrationNew = props => {
  const appSettings = require('../../Settings/Components/TimeRegistration/TimeRegistrationNew.json');

  const [projects, setProjects] = useState([]);

  const [SelectedProject, setProjectId] = useState(0);
  const [Date, setDate] = useState(null);
  const [AntalMinuter, SetAntalMinuter] = useState(0);
  const [Beskrivning, setBeskrivning] = useState(0);

  useEffect(()=>{
    fetchAllProjects().then( result => {
      setProjects(result)
     }
    )
    },
    []
  );

  const onRegister = ()=>{
    appSettings.PostDTO.Beskrivning = Beskrivning;
    appSettings.PostDTO.Datum = Date;
    appSettings.PostDTO.AntalMinuter = AntalMinuter;
    appSettings.PostDTO.ProjectId = SelectedProject;
    fetch(
      appSettings.apiUrl,
      {
        method: appSettings.fetchMethod,
        headers: appSettings.fetchHeaders,
        body: JSON.stringify(appSettings.PostDTO)
      }
      ).then(
        result =>
        {
          console.log(result);
          props.changeActivePage(props.startPage)
        }
      )
    }

  return (
    <section>
      <form>
        <div className='formGroup'>
          <label className='formLabel'>Antal Minuter</label>
          <input className='formInput' onChange={e=>SetAntalMinuter(e.target.value)} type="number"/>
        </div>
        
        <div className='formGroup'>
          <label className='formLabel'>Datum</label>
          <input className='formInput' onChange={e=>setDate(e.target.value)} type="date"/>
        </div>

        <div className='formGroup'>
          <label className='formLabel'>Project</label>
          <select className='formInput' onChange={e=>setProjectId(e.target.value)}>
            <option disabled={true} selected={true} value={0}>Välj ett Projekt</option>
            {projects.map( proj=>
              <option key={proj.projectId} value={proj.projectId}>{proj.projectName}</option>
            )}
          </select>
        </div>

        <div className='formGroup'>
          <label className='formLabel'>Beskrivning</label>
          <textarea className='formTextArea' onChange={e=>setBeskrivning(e.target.value) } rows="4"></textarea>
        </div>

        <input className='formSubmit' onClick={()=>onRegister()} type="submit" value="Create"/>
      </form>
    </section>
  )
}
