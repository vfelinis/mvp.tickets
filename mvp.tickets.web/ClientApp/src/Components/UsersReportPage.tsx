import { observer } from 'mobx-react-lite';
import { FC, useEffect } from 'react';
import { useRootStore } from '../Store/RootStore';

interface IUsersReportPageProps {
}

const UsersReportPage: FC<IUsersReportPageProps> = (props) => {
  const store = useRootStore()
  useEffect(() => {
    store.userStore.getUsersReport()
  }, []);
  return <>
    {store.userStore.usersReport.map(user => <div key={user.id}>{user.email}</div>)}
  </>
}

export default observer(UsersReportPage)
