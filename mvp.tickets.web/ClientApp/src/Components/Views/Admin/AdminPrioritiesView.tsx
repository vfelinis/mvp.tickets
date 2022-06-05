import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminPrioritiesViewProps {
}

const AdminPrioritiesView: FC<IAdminPrioritiesViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Приоритеты
        </Typography>
    </>;
};

export default AdminPrioritiesView;
