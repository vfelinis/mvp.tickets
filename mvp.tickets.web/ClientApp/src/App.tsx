import {Navigate, useRoutes} from 'react-router-dom';
import HomeView from './Components/HomeView';
import Layout from './Components/Layout';
import LoginView from './Components/LoginView';
import NotFoundView from './Components/NotFoundView';

export const App: React.FC = (): JSX.Element => {
  const routes = {
    path: '/',
    element: <Layout />,
    children: [
      {path: '*', element: <Navigate to='/404' />},
      {path: '/', element: <HomeView />},
      {path: 'login', element: <LoginView />},
      {path: '404', element: <NotFoundView />},
    ],
  };

  const routing = useRoutes([routes]);

  return <>{routing}</>;
}
