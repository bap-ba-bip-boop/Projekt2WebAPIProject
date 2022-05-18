import React, { useState, useEffect } from 'react';

import {fetchRegs} from './Data/RegData';

import { TimeRegistration } from './TimeRegistration';

export const RegList = () => {

    const [registrations, setRegistrations] = useState([]);

    useEffect( ()=>
        {
            fetchRegs().then( result => {
                setRegistrations(result)
            }
            )
        },
        []
    );

    let keyList = 1;
    return (
      <div>
          {registrations.map( reg=>
              <TimeRegistration
                  projectName ={reg.projectName}
                  key = {keyList++}
                  datum = {reg.datum}
                  antalMinuter = {reg.antalMinuter}
                  />
          )}
      </div>
    )
}
