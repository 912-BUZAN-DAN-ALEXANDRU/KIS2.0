import * as React from 'react';
import { useState } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';


import './custom.css'
import SignUp from './signup';
import LogIn from './login';
import Home from './home';
import PostUpload from './postupload';

import { NavbarLogged, NavbarGuest } from './navbar';
import useToken from './useToken';

function App() {
    const { token, setToken }= useToken();
    console.log(token);
    if (!token) {
        return <Router>
            <div className="App">
                <NavbarGuest />
                <div className='container'>
                    <Switch>
                        <Route exact path='/'><LogIn setToken={setToken} /></Route>
                        <Route exact path='/register' component={SignUp}  />
                    </Switch>
                </div>
            </div>
        </Router>


    }

    return (
        <Router>
            <div className="App">
                <NavbarLogged />
                <div className='container'>
                    <Switch>
                        <Route exact path='/'> <Home token={token}/> </Route>
                        <Route exact path='/create'><PostUpload token={token}/></Route>
                    </Switch>
                </div>
            </div>
        </Router>
    )
}

export default App;
