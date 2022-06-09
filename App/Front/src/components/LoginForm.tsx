import React from "react";
import {Layout} from "./Layout";
import {AllBeats} from "./AllBeats";
import {Button, Container, FormGroup, Input, Label} from "reactstrap";

export const LoginForm: React.FC = ({}) => {
    return (
        <div className="container-fluid col-4" >
            <FormGroup>
                <Label>Логин</Label>
                <Input>

                </Input>
                <Label>Пароль</Label>
                <Input>

                </Input>
            </FormGroup>
            <Button>Войти</Button>
        </div>
    )
}

