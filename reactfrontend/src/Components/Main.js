import React from 'react'

import {RegList} from './RegList';
import { TimeRegistrationForm } from './TimeRegistrationForm';

export const Main = props => {

    return (
      <main className='siteMain'>
          {props.activePage === 'Start' && <RegList/>}
          {props.activePage === 'newReg' && <TimeRegistrationForm/>}
      </main>
    )
}
