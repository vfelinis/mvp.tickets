import { FC, useEffect } from 'react';
import { Navigate, useRoutes } from 'react-router-dom';
import HomeView from './Components/Views/HomeView';
import Layout from './Components/Shared/Layout';
import LoginView from './Components/Views/LoginView';
import NotFoundView from './Components/Views/NotFoundView';
import { useRootStore } from './Store/RootStore';
import { IUserModel } from './Store/UserStore';

interface IAppProps {
  user: IUserModel | null
}

export const App: FC<IAppProps> = (props) => {
  const store = useRootStore();

  useEffect(() => {
    if (props.user !== null) {
      store.userStore.setUser(props.user);
    }
  }, []);

  const mainRoutes = {
    path: '/',
    element: <Layout />,
    children: [
      { path: '*', element: <Navigate to="/404" /> },
      { path: '/', element: <HomeView /> },
      { path: '404', element: <NotFoundView /> },
    ],
  };
  const loginRoutes = {
    path: '/login',
    element: <LoginView />
  };

  const routing = useRoutes([mainRoutes, loginRoutes]);

  return <>{routing}</>;
}
