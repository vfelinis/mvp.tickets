import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminStatusesViewProps {
}

const AdminStatusesView: FC<IAdminStatusesViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Статусы
        </Typography>
    </>;
};

export default AdminStatusesView;
