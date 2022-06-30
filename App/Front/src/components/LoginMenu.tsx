import React, {Component, Fragment} from "react";
import {NavItem, NavLink} from "reactstrap";
import {Link} from 'react-router-dom';

function IsAuthenticated(): Promise<boolean> {
    return fetch('/admin/IsAuthenticated').then(x => x.json())
}

export const LoginMenu: React.FC = ({}) => {
    const [isAuthenticated, setAuthenticated] = React.useState<boolean>()

    React.useEffect(() => {
        IsAuthenticated().then(setAuthenticated);
    }, []);

    if (isAuthenticated) {
        return (
            <Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/bag">Корзина</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/">Аккаунт</NavLink>
                </NavItem>
            </Fragment>
        )
    } else {
        return (
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