import React from "react";
import {Alert, Button, FormGroup, Input, Label} from "reactstrap";
import {useNavigate} from "react-router-dom";

export const RegisterForm: React.FC = ({}) => {
    const [username, setLogin] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [confirmPassword, setConfirmPassword] = React.useState('');

    const navigate = useNavigate();
    const goHome = () => navigate('/');
    
    const onRegister = () => {
        const model: RegisterModel = {
            username,
            password,
            confirmPassword
        };
        postRegister(model);
        goHome();
    }
    
    return (
        <div className="container-fluid col-4" >
            <FormGroup>
                <Label>Логин</Label>
                <Input type="text" value={username} onChange={e => setLogin(e.target.value)}>

                </Input>
                <Label>Пароль</Label>
                <Input type="text" value={password} onChange={e => setPassword(e.target.value)}>

                </Input>
                <Label>Подтвердите пароль</Label>
                <Input type="text" value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)}>

                </Input>
            </FormGroup>
            <Button onClick={onRegister}>Регистрация</Button>
        </div>
    )
}

function postRegister(model: RegisterModel)
{
    fetch('/admin/Register',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        }
    );
}

//TODO сделать так, чтоб это всё работало

interface RegisterModel{
    username: string;
    password: string;
    confirmPassword: string;
}