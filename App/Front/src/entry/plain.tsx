import {App} from "components/App";
import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import {User} from "../components/User";
import 'bootstrap/dist/css/bootstrap.css';
import {LoginForm} from "../components/LoginForm";


ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter basename={"/"}>
            <App />
        </BrowserRouter>
    </React.StrictMode>, 
    document.getElementById('root')
);
