import React, { useState, useEffect } from 'react'

import {fetchProjects} from './Data/ProjectData';
/*
public string? Beskrivning { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
    public int ProjectId { get; set; }
*/
export const TimeRegistrationForm = props => {
  const url = 'https://localhost:7045/tidsregistrering';

  const [projects, setProjects] = useState([]);

  const [SelectedProject, setProjectId] = useState(0);
  const [Date, setDate] = useState(null);
  const [AntalMinuter, SetAntalMinuter] = useState(0);
  const [Beskrivning, setBeskrivning] = useState(0);

  useEffect(()=>{
    fetchProjects().then( result => {
      setProjects(result)
     }
    )
    },
    []
  );

  const onRegister = ()=>{
    var result = {//magic strings
      "Beskrivning": Beskrivning,
      "Datum": Date,
      "AntalMinuter": AntalMinuter,
      "ProjectId": SelectedProject
    }
    console.log(result);
    fetch(
      url,
      {
        method: "POST",
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(result)
      }
      ).then(
        result =>
        {
          console.log(result);
        }
      )
      props.changeActivePage('Start')
      window.location.reload(false);
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
            <option disabled={true} selected={true}>Välj ett Projekt</option>
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
