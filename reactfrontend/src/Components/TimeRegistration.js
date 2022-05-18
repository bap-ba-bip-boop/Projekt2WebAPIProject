import React from 'react'

export const TimeRegistration = props => {
  return (
    <div className='timeRegistrationPanel'>
        <p className='timeregTitle'>{props.projectName}</p>
        <p className='timeregPropertyLabel'>{props.datum.split("T")[0]}</p>
        <p className='timeregPropertyLabel'>{props.antalMinuter} Minutes</p>
    </div>
  )
}
