import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminTemplatesViewProps {
}

const AdminTemplatesView: FC<IAdminTemplatesViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Шаблоны
        </Typography>
    </>;
};

export default AdminTemplatesView;
