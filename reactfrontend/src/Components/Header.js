import React from 'react'

export const Header = props => 
    <header className='siteHeader'>
        <a className='headerLink' href="#"><span className='headerSpan' onClick={()=>props.changeActivePage(props.startPage)}>Time</span>Entry.com</a>
    </header>
