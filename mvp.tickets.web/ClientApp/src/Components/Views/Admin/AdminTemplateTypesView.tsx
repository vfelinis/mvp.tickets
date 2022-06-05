import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminTemplateTypesViewProps {
}

const AdminTemplateTypesView: FC<IAdminTemplateTypesViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Типы шаблонов
        </Typography>
    </>;
};

export default AdminTemplateTypesView;
