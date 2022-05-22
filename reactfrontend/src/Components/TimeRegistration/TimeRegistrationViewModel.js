import React from 'react'

export const TimeRegistrationViewModel = props => {
  return (
    <a href="#" onClick=
      {
        ()=> {
          props.setCurrentTimeReg(props.id);
          props.changeActivePage(props.redirectPage);
        }
      } className='timeRegistrationPanel'>
        <p className='timeregTitle'>{props.projectName}</p>
        <p className='timeregPropertyLabel'>{props.datum.split("T")[0]}</p>
        <p className='timeregPropertyLabel'>{props.antalMinuter} Minutes</p>
    </a>
  )
}
