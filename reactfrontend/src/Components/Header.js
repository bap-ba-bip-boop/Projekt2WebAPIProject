import React from 'react'

export const Header = props => 
    <header className='siteHeader'>
        <a className='headerLink' href="#"onClick={()=>props.changeActivePage(props.startPage)}><span className='headerSpan'>Time</span>Entry.com</a>
    </header>
