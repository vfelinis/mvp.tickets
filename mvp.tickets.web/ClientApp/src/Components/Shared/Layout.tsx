import { FC } from 'react';
import { Outlet, Link, Navigate } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import Error from './Error';
import { useRootStore } from '../../Store/RootStore';
import { UIRoutesHelper } from '../../Helpers/UIRoutesHelper';

interface ILayoutProps {
}

const Layout: FC<ILayoutProps> = (props) => {
    const store = useRootStore();
    return <>
        {store.userStore.user === null ? <Navigate to={UIRoutesHelper.login} replace={true} /> : null}
        <Error />
        <nav>
            <ul>
                <li>
                    <Link to="/">Главная</Link>
                </li>
                <li>
                    <button onClick={() => store.userStore.logout()}>Выйти</button>
                </li>
            </ul>
        </nav>
        <Outlet />
    </>;
};

export default observer(Layout);
