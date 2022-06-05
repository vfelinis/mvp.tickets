import { Typography } from '@mui/material';
import { FC } from 'react';

interface ITicketsViewProps {
}

const TicketsView: FC<ITicketsViewProps> = (props) => {
  return <>
    <Typography variant="h6" component="div">
      Мои заявки
    </Typography>
  </>;
};

export default TicketsView;
