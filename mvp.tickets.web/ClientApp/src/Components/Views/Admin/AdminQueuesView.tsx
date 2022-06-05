import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminQueuesViewProps {
}

const AdminQueuesView: FC<IAdminQueuesViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Очереди
        </Typography>
    </>;
};

export default AdminQueuesView;
