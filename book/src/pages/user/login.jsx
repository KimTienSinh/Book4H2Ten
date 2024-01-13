import React from 'react';
import {GoogleLogo,FacebookLogo} from "phosphor-react";
import { Link } from 'react-router-dom';

import "./login.css";

export const Login = () => {
  return (
    <div className='formLogin'>
        <form action="#" className='loginForm'>
            <h1>Login</h1>
            <hr />
            <div className='box'>
                <div className='boxInput'>
                    <div className='textName'>Username</div>
                    <input type="text"  className='inputLogin'/>
                </div>
                <div className='boxInput'>
                    <div className='textName'>Password</div>
                    <input type="password" className='inputLogin'/>
                </div>
            </div>
            <input type="submit" value="Login" className='btnSubmit'/>
            <hr />
            <p>Other Sign-in</p>
            <p className='otherLoginMethod'><Link><GoogleLogo size={28}/></Link><h3>Or</h3><Link><FacebookLogo size={28}/></Link></p>
        </form>
    </div>
  );
};
