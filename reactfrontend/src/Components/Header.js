import React from 'react'

export const Header = props => 
    <header className='siteHeader'>
        <a className='headerLink' href="#"onClick={()=>props.changeActivePage('Start')}><span className='headerSpan'>Time</span>Entry.com</a>
    </header>
