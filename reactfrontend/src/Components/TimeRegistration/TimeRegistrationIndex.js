import React, { useState, useEffect } from 'react';

import {fetchAllRegs} from '../Data/AllRegData';

import {TimeRegistrationViewModel } from './TimeRegistrationViewModel';

export const TimeRegistrationIndex = props => {

    const [registrations, setRegistrations] = useState([]);

    useEffect( ()=>
        {
            fetchAllRegs().then( result => {
                setRegistrations(result)
                console.log(result)
            }
            )
        },
        []
    );
    return (
      <div>
          {registrations.map( reg=>
              <TimeRegistrationViewModel
                setCurrentTimeReg={props.setCurrentTimeReg}
                projectName ={reg.projectName}
                key = {reg.tidsRegistreringId}
                id = {reg.tidsRegistreringId}
                datum = {reg.datum}
                antalMinuter = {reg.antalMinuter}
                />
          )}
      </div>
    )
}
