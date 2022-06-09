import React from 'react';
import {AllBeats} from "./AllBeats";
import {Layout} from "./Layout";

import './style.sass'
import {Route, Routes} from "react-router-dom";
import {User} from "./User";
import {LoginForm} from "./LoginForm";
import {RegisterForm} from "./RegisterForm";

export const App: React.FC = ({}) => {
    return (
        <Layout>
            <Routes>
                <Route path="/" element={<AllBeats />} />
                <Route path="/User" element={<User />}/>
                <Route path="/Admin/Login" element={<LoginForm />} />
                <Route path="/Admin/Register" element={<RegisterForm />} />
            </Routes>
        </Layout>
    )
}

