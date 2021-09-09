import * as React from 'react';
import { Link } from 'react-router-dom'
const NavbarGuest = ( ) => {

    return (
        <nav className="navbar  bg-primary">
            <h1>
                KIS - Keep It Simple
            </h1>
            <ul>
                <li>
                    <Link to='/login'>Log In</Link>
                </li>
                <li>
                    <Link to='/register'>Register</Link>
                </li>

            </ul>
        </nav>
    )

}
NavbarGuest.defaultProps = {
    title: 'Gravity Well Gadgets'
};

const NavbarLogged = () => {

    return (
        <nav className="navbar  bg-primary">
            <h1>
                KIS - Keep It Simple
            </h1>
            <ul>
                <li>
                    <Link to='/'>Home</Link>
                </li>
            </ul>
        </nav>
    )

}
NavbarLogged.defaultProps = {
    title: 'Gravity Well Gadgets'
};

export { NavbarLogged, NavbarGuest }
