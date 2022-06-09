import React, { Component } from 'react';
import {Collapse, Container, Nav, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import { Link } from 'react-router-dom';
import {AllBeats} from "./AllBeats";
import {LoginMenu} from "./LoginMenu";

export const NavMenu: React.FC = ({}) => {
    const [collapsed, setCollapsed] = React.useState<boolean>(false)

    function toggleNavbar(){
        setCollapsed(!collapsed);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow-3 mb-3" container={true} light>
                    <NavbarBrand tag={Link} to="/">Beatstore крутого саундпродюссера</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
                        <Nav className="navbar-nav flex-grow">
                            <LoginMenu isAuthenticated={true}>
                            </LoginMenu>
                        </Nav>
                    </Collapse>
            </Navbar>
        </header>
    );
}



