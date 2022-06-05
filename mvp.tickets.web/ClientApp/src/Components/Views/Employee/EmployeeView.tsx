import { Typography } from '@mui/material';
import { FC } from 'react';

interface IEmployeeViewProps {
}

const EmployeeView: FC<IEmployeeViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Заявки
        </Typography>
    </>;
};

export default EmployeeView;
