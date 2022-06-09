import React from "react";
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

interface Props {
    children: React.ReactNode
}

export const Layout: React.FC<Props> = (props:Props) => {
    return (
        <div>
            <NavMenu />
            <Container>
                {props.children}
            </Container>
        </div>
    );
}