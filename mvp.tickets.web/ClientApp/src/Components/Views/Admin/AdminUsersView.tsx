import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminUsersViewProps {
}

const AdminUsersView: FC<IAdminUsersViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Пользователи
        </Typography>
    </>;
};

export default AdminUsersView;
