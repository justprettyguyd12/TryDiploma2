import { App } from "components/App";
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { User } from "../components/User";
ReactDOM.render(React.createElement(React.StrictMode, null,
    React.createElement(BrowserRouter, null,
        React.createElement(Routes, null,
            React.createElement(Route, { path: "/", element: React.createElement(App, null) }),
            React.createElement(Route, { path: "/User", element: React.createElement(User, null) })))), document.getElementById('root'));
