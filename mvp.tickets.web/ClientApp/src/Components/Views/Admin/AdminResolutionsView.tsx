import { Typography } from '@mui/material';
import { FC } from 'react';

interface IAdminResolutionsViewProps {
}

const AdminResolutionsView: FC<IAdminResolutionsViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Резолюции
        </Typography>
    </>;
};

export default AdminResolutionsView;
