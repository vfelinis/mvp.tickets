import { FC, useState } from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import { visuallyHidden } from '@mui/utils';
import { formatDistanceToNow } from 'date-fns';
import { ru } from 'date-fns/locale';
import IconButton from '@mui/material/IconButton';

interface ITableComponentProps {
    table: ITable
}

export enum ColumnType {
    String = 1,
    Number = 2,
    Date = 3,
    Boolean = 4
}

interface ITableColumn {
    field: string
    label: string
    type: ColumnType
    sortable: boolean
}

interface ITableOptions {
    sortable: boolean
    editRoute: Function | undefined
}

interface ITable {
    options: ITableOptions
    columns: ITableColumn[]
    rows: any[]
}

const TableComponent: FC<ITableComponentProps> = (props) => {
    const [direction, setDirection] = useState(1);
    const [orderBy, setOrderBy] = useState(props.table.columns[0].field);

    const getValue = (item: any, column: ITableColumn): any => {
        return column.type === ColumnType.Date
            ? new Date(item[column.field])
            : item[column.field];
    };

    const getFormatedValue = (item: any, column: ITableColumn): any => {
        switch (column.type) {
            case ColumnType.Date:
                return formatDistanceToNow(new Date(item[column.field]), { addSuffix: true, locale: ru });
            case ColumnType.Boolean:
                return item[column.field] === true ? 'да' : 'нет';
            default:
                return item[column.field];
        }
    };

    const order = direction === 1 ? 'asc' : 'desc';

    const sort = (field: string): void => {
        setDirection(direction * -1);
        setOrderBy(field);
    };

    const sortedColumn = props.table.columns.find(s => s.field === orderBy);

    let sortedRows = props.table.rows;
    if (sortedColumn) {
        sortedRows = sortedRows.sort((a: any, b: any) => {
            const aValue = getValue(a, sortedColumn);
            const bValue = getValue(b, sortedColumn);
            return aValue > bValue ? direction : bValue > aValue ? direction * -1 : 0;
        });
    }

    return <>
        <TableContainer sx={{ mt: 2 }} component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        {props.table.columns.map((column, i) => {
                            return props.table.options.sortable && column.sortable
                                ? <TableCell
                                    key={i}
                                    sortDirection={orderBy === column.field ? order : false}
                                >
                                    <TableSortLabel
                                        active={orderBy === column.field}
                                        direction={orderBy === column.field ? order : 'asc'}
                                        onClick={() => sort(column.field)}
                                    >
                                        {column.label}
                                        {orderBy === column.field ? (
                                            <Box component="span" sx={visuallyHidden}>
                                                {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                            </Box>
                                        ) : null}
                                    </TableSortLabel>
                                </TableCell>
                                : <TableCell>{column.label}</TableCell>;
                        })}
                        {props.table.options.editRoute && <TableCell></TableCell>}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {sortedRows.map((row, i) => (
                        <TableRow
                            key={i}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            {props.table.columns.map((column, i) => (
                                <TableCell key={i}>{getFormatedValue(row, column)}</TableCell>
                            ))}
                            {props.table.options.editRoute &&
                                <TableCell>
                                    <IconButton component={Link} to={props.table.options.editRoute(row)}>
                                        <EditIcon />
                                    </IconButton>
                                </TableCell>
                            }
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    </>;
};

export default TableComponent;
