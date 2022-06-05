import { FC, useEffect } from 'react';
import { Navigate, useRoutes } from 'react-router-dom';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import HomeView from './Components/Views/HomeView';
import Layout from './Components/Shared/Layout';
import LoginView from './Components/Views/LoginView';
import NotFoundView from './Components/Views/NotFoundView';
import { useRootStore } from './Store/RootStore';
import { IUserModel } from './Models/User';
import { UIRoutesHelper } from './Helpers/UIRoutesHelper';
import TicketsView from './Components/Views/TicketsView';
import CreateTicketView from './Components/Views/CreateTicketView';
import EmployeeView from './Components/Views/Employee/EmployeeView';
import ProtectedRoute from './Components/Shared/ProtectedRoute';
import { Permissions } from './Models/Permissions';
import AdminUsersView from './Components/Views/Admin/AdminUsersView';
import AdminCategoriesView from './Components/Views/Admin/AdminCategoriesView';
import AdminCategoriesCreateView from './Components/Views/Admin/AdminCategoriesCreateView';
import AdminCategoriesUpdateView from './Components/Views/Admin/AdminCategoriesUpdateView';
import AdminPrioritiesView from './Components/Views/Admin/AdminPrioritiesView';
import AdminQueuesView from './Components/Views/Admin/AdminQueuesView';
import AdminStatusRulesView from './Components/Views/Admin/AdminStatusRulesView';
import AdminStatusesView from './Components/Views/Admin/AdminStatusesView';
import AdminResolutionsView from './Components/Views/Admin/AdminResolutionsView';
import AdminTemplateTypesView from './Components/Views/Admin/AdminTemplateTypesView';
import AdminTemplatesView from './Components/Views/Admin/AdminTemplatesView';

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
    path: UIRoutesHelper.home.getRoute(),
    element: <ProtectedRoute permissions={Permissions.User} children={<Layout />} user={props.user} />,
    children: [
      { path: '*', element: <Navigate to={UIRoutesHelper.notFound.getRoute()} /> },
      { path: UIRoutesHelper.home.path, element: <HomeView /> },
      { path: UIRoutesHelper.tickets.path, element: <TicketsView /> },
      { path: UIRoutesHelper.ticketsCreate.path, element: <CreateTicketView /> },
      { path: UIRoutesHelper.adminUsers.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminUsersView />} user={props.user} /> },
      { path: UIRoutesHelper.adminCategories.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminCategoriesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminCategoriesCreate.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminCategoriesCreateView />} user={props.user} /> },
      { path: UIRoutesHelper.adminCategoriesUpdate.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminCategoriesUpdateView />} user={props.user} /> },
      { path: UIRoutesHelper.adminPriorities.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminPrioritiesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminQueues.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminQueuesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminStatusRules.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminStatusRulesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminStatuses.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminStatusesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminResolutions.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminResolutionsView />} user={props.user} /> },
      { path: UIRoutesHelper.adminTemplateTypes.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminTemplateTypesView />} user={props.user} /> },
      { path: UIRoutesHelper.adminTemplates.path, element: <ProtectedRoute permissions={Permissions.Admin} children={<AdminTemplatesView />} user={props.user} /> },
      { path: UIRoutesHelper.employee.path, element: <ProtectedRoute permissions={Permissions.Employee} children={<EmployeeView />} user={props.user} /> },
      { path: UIRoutesHelper.notFound.path, element: <NotFoundView /> },
    ],
  };
  const loginRoutes = {
    path: UIRoutesHelper.login.getRoute(),
    element: <LoginView />
  };

  const routing = useRoutes([mainRoutes, loginRoutes]);

  return <>{routing}</>;
}
