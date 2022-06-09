import { FC, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { UIRoutesHelper } from '../../../Helpers/UIRoutesHelper';
import { useRootStore } from '../../../Store/RootStore';
import { ICategoryModel } from '../../../Models/Category';
import TableComponent, { ColumnType, tableColumnBooleanSearchOptions } from '../../Shared/TableComponent';


interface IAdminCategoriesViewProps {
}

const AdminCategoriesView: FC<IAdminCategoriesViewProps> = (props) => {
    const store = useRootStore();

    useEffect(() => {
        store.categoryStore.getCategories();
    }, []);

    return <>
        <Typography variant="h6" component="div">
            Категории
        </Typography>
        <Button variant="contained" component={Link} to={UIRoutesHelper.adminCategoriesCreate.getRoute()}>Создать</Button>
        <TableComponent table={{
            options: {
                editRoute: (row: ICategoryModel): string => UIRoutesHelper.adminCategoriesUpdate.getRoute(row.id),
                isServerSide: false,
                total: store.categoryStore.categories.length,
            },
            columns: [
                { field: 'id', label: 'Id', type: ColumnType.Number, sortable: true, searchable: true },
                { field: 'name', label: 'Название', type: ColumnType.String, sortable: true, searchable: true },
                {
                    field: 'isRoot', label: 'Корневая', type: ColumnType.Boolean, sortable: false, searchable: this,
                    searchOptions: tableColumnBooleanSearchOptions
                },
                {
                    field: 'isActive', label: 'Активная', type: ColumnType.Boolean, sortable: false, searchable: this,
                    searchOptions: tableColumnBooleanSearchOptions
                },
                // { field: 'dateCreated', label: 'Создана', type: ColumnType.Date, sortable: true },
                // { field: 'dateModified', label: 'Обновлена', type: ColumnType.Date, sortable: true },
            ],
            rows: [...store.categoryStore.categories]
        }} />
    </>;
};

export default observer(AdminCategoriesView);
