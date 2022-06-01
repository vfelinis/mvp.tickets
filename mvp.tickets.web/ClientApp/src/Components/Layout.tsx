import * as React from 'react';
import { Outlet, Link } from "react-router-dom";

interface ILayoutProps {
}

const Layout: React.FunctionComponent<ILayoutProps> = (props) => {
    return <>
        <nav>
            <ul>
                <li>
                    <Link to="/">Главная</Link>
                </li>
                <li>
                    <Link to="/login">Войти</Link>
                </li>
            </ul>
        </nav>
        <Outlet />
    </>;
};

export default Layout;
