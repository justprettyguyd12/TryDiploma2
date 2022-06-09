import React, {Component, Fragment} from "react";
import {NavItem, NavLink} from "reactstrap";
import { Link } from 'react-router-dom';

type State = {
    isAuthenticated: boolean;
}

function IsAuthenticated() : Promise<boolean> {
    return fetch('/admin/IsAuthenticated').then(x => x.json())
}

export class LoginMenu extends Component<State> {
    state : State = { isAuthenticated:  false};
    
    componentDidMount() {
        IsAuthenticated().then(x => this.setState(x))
    }

    render() {
        const {isAuthenticated} = this.state
        
        if (isAuthenticated.valueOf())
        {
            return(
                <Fragment>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/">Корзина</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/">Аккаунт</NavLink>
                    </NavItem>
                </Fragment>
            )
        }
        else
        {
            return(
                <Fragment>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/admin/Register">Регистрация</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/admin/Login">Войти</NavLink>
                    </NavItem>
                </Fragment>
            );
        }
    }
}