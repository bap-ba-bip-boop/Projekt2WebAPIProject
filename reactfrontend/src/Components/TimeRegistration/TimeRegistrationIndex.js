import React, { useState, useEffect } from 'react';

import { getData } from '../Data/JSONData';

import {TimeRegistrationViewModel } from './TimeRegistrationViewModel';

export const TimeRegistrationIndex = props => {
    const [registrations, setRegistrations] = useState([]);

    useEffect( ()=>
        {
            getData(props.getAllRegUrl).then( result => {
                setRegistrations(result)
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
                changeActivePage={props.changeActivePage}
                projectName ={reg.projectName}
                key = {reg.tidsRegistreringId}
                id = {reg.tidsRegistreringId}
                datum = {reg.datum}
                antalMinuter = {reg.antalMinuter}
                redirectPage={props.redirectPage}
                />
          )}
      </div>
    )
}
