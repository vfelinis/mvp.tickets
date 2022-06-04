import axios from 'axios';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { App } from './App'
import { ApiRoutesHelper } from './Helpers/ApiRoutesHelper';
import { IBaseQueryResponse } from './Models/Base';
import { IUserModel } from './Store/UserStore';

const init = (user: IUserModel | null) => {
  const element = (
    <BrowserRouter>
      <App user={user} />
    </BrowserRouter>
  );

  const container = document.getElementById('root');
  const root = createRoot(container!);
  root.render(element);
};
let user: IUserModel;
axios.post<IBaseQueryResponse<IUserModel>>(ApiRoutesHelper.user.current)
  .then(response => {
    if (response.data.isSuccess) {
      init(response.data.data);
    } else {
      init(null);
    }
  })
  .catch(error => {
    init(null);
  });
