import { Typography } from '@mui/material';
import { FC } from 'react';

interface ICreateTicketViewProps {
}

const CreateTicketView: FC<ICreateTicketViewProps> = (props) => {
    return <>
        <Typography variant="h6" component="div">
            Создать заявку
        </Typography>
    </>;
};

export default CreateTicketView;
