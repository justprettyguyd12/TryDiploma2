import React from "react";
import {Layout} from "./Layout";
import {AllBeats} from "./AllBeats";
import {Alert, Button, Container, FormGroup, Input, Label} from "reactstrap";
import {Route, Navigate, useNavigate} from "react-router-dom";
import {LoginMenu} from "./LoginMenu";

export const LoginForm: React.FC = ({}) => {
    const [username, setLogin] = React.useState<string>('');
    const [password, setPassword] = React.useState<string>('');
    const [alertText, setAlertText] = React.useState<string>('');
    const [alert, setAlert] = React.useState<boolean>(false);
    
    const navigate = useNavigate();
    const goHome = () => navigate('/');
    
    const onLogin = () => {
        const model: LoginModel = {
            username,
            password
        };
        postLogin(model).then(responce => {
            if (responce.ok) {
                //todo обновить навбар
                goHome();
            }
            else{
                return responce.text();
            }
        }).then((data) => {
            setAlertText(data);
            setAlert(true)
        })
    }
    
    const toggleAlert = () => {
        setAlert(false)
    }
    
    return (
        <div className="container-fluid col-4" >
            <FormGroup>
                <Label>Логин</Label>
                <Input type="text" value={username} onChange={e => setLogin(e.target.value)} />
                <Label>Пароль</Label>
                <Input type="text" value={password} onChange={e => setPassword(e.target.value)}/>
            </FormGroup>
            <Button type="submit" onClick={onLogin}>Войти</Button>
            <Alert color="danger" isOpen={alert} toggle={toggleAlert} className="mt-2">{alertText}</Alert>
        </div>
    )
}

function postLogin(model: LoginModel) {
    return fetch('/admin/Login',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        })
    }

interface LoginModel {
    username: string;
    password: string;
}

