import { FC, useEffect, useState, useLayoutEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import { visuallyHidden } from '@mui/utils';
import { formatDistanceToNow } from 'date-fns';
import { ru } from 'date-fns/locale';
import { UIRoutesHelper } from '../../../Helpers/UIRoutesHelper';
import { useRootStore } from '../../../Store/RootStore';
import { ICategoryModel } from '../../../Models/Category';
import IconButton from '@mui/material/IconButton';


interface IAdminCategoriesViewProps {
}

const AdminCategoriesView: FC<IAdminCategoriesViewProps> = (props) => {
    const store = useRootStore();
    const [categories, setCategories] = useState<ICategoryModel[]>([]);
    const [direction, setDirection] = useState(1);
    const [orderBy, setOrderBy] = useState('');

    useEffect(() => {
        store.categoryStore.getCategories();
    }, []);

    useLayoutEffect(() => {
        setCategories([...store.categoryStore.categories]);
    }, [store.categoryStore.categories]);

    const dateFormat = (date: Date): string => {
        return formatDistanceToNow(new Date(date), { addSuffix: true, locale: ru });
    };

    const getValue = (item: any, field: string, isDate: boolean): any => {
        return isDate
            ? new Date(item[field])
            : item[field];
    };

    const order = direction === 1 ? 'asc' : 'desc';

    const sort = (field: string, isDate: boolean = false): void => {
        setCategories(categories.sort((a: any, b: any) => (getValue(a, field, isDate) > getValue(b, field, isDate)) ? direction : ((getValue(b, field, isDate) > getValue(a, field, isDate)) ? direction * -1 : 0)));
        setDirection(direction * -1);
        setOrderBy(field);
    };

    const headCells = [
        { id: 'id', label: 'Id', isDate: false },
        { id: 'name', label: 'Название', isDate: false },
        { id: 'isRoot', label: 'Корневая', isDate: false },
        { id: 'isActive', label: 'Активная', isDate: false },
        { id: 'dateCreated', label: 'Создано', isDate: true },
        { id: 'dateModified', label: 'Обновлено', isDate: true },
    ];

    return <>
        <Typography variant="h6" component="div">
            Категории
        </Typography>
        <Button variant="contained" component={Link} to={UIRoutesHelper.adminCategoriesCreate.getRoute()}>Создать</Button>
        <TableContainer sx={{ mt: 2 }} component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        {headCells.map((headCell) => (
                            <TableCell
                                key={headCell.id}
                                sortDirection={orderBy === headCell.id ? order : false}
                            >
                                <TableSortLabel
                                    active={orderBy === headCell.id}
                                    direction={orderBy === headCell.id ? order : 'asc'}
                                    onClick={() => sort(headCell.id, headCell.isDate)}
                                >
                                    {headCell.label}
                                    {orderBy === headCell.id ? (
                                        <Box component="span" sx={visuallyHidden}>
                                            {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                        </Box>
                                    ) : null}
                                </TableSortLabel>
                            </TableCell>
                        ))}
                        <TableCell></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {categories.map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell>{row.id}</TableCell>
                            <TableCell>{row.name}</TableCell>
                            <TableCell>{row.isRoot ? 'да' : 'нет'}</TableCell>
                            <TableCell>{row.isActive ? 'да' : 'нет'}</TableCell>
                            <TableCell>{dateFormat(row.dateCreated)}</TableCell>
                            <TableCell>{dateFormat(row.dateModified)}</TableCell>
                            <TableCell>
                                <IconButton component={Link} to={UIRoutesHelper.adminCategoriesUpdate.getRoute(row.id)}>
                                    <EditIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    </>;
};

export default observer(AdminCategoriesView);
