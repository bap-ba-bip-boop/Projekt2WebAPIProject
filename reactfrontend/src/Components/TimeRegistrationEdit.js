import React, { useState, useEffect } from 'react'

import { fetchOneReg } from './Data/OneRegData';

export const TimeRegistrationEdit = props => {
  const url = `https://localhost:7045/tidsregistrering/${props.currentRegId}`;

  const [timeReg, setTimeReg] = useState([]);

  useEffect(()=>{
    fetchOneReg(props.currentRegId).then( result => {
      setTimeReg(result)
      SetDatum(result.datum.split("T")[0]);
      SetAntalMinuter(result.antalMinuter);
      SetBeskrivning(result.beskrivning);
     }
    )
    },
    []
  );
  
  //const [SelectedProject, setProjectId] = useState(0);
  const [Datum, SetDatum] = useState(timeReg.datum);
  const [AntalMinuter, SetAntalMinuter] = useState(timeReg.antalMinuter);
  const [Beskrivning, SetBeskrivning] = useState(timeReg.beskrivning);

  const onRegister = ()=>{
    var result = {//magic strings
      "Beskrivning": Beskrivning,
      "Datum": Datum,
      "AntalMinuter": AntalMinuter
    }
    fetch(
      url,
      {
        method: "PUT",
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
      props.changeActivePage(props.startPage)
      //window.location.reload(false);//hitta ett bättre sätt att ladda om listan?
  }

  return (
    <section>
      <form>
        <div className='formGroup'>
          <label className='formLabel'>Antal Minuter</label>
          <input className='formInput' onChange={e=>SetAntalMinuter(e.target.value)} type="number" value={AntalMinuter}/>
        </div>
        
        <div className='formGroup'>
          <label className='formLabel'>Datum</label>
          <input className='formInput' onChange={e=>SetDatum(e.target.value)} type="date" value={Datum}/>
        </div>

        <div className='formGroup'>
          <label className='formLabel'>Beskrivning</label>
          <textarea className='formTextArea' onChange={e=>SetBeskrivning(e.target.value) } rows="4" value={Beskrivning}></textarea>
        </div>

        <input className='formSubmit' onClick={()=>onRegister()} type="submit" value="Edit"/>
      </form>
    </section>
  )
}
