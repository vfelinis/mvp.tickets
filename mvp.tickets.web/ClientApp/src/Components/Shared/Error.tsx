import { FC } from 'react';
import { useRootStore } from '../../Store/RootStore';
import { observer } from 'mobx-react-lite';

interface IErrorProps {
}

const Error: FC<IErrorProps> = (props) => {
    const store = useRootStore();
    return <>
        {store.errorStore.errors.map(s => <div>{s}</div>)}
    </>;
};

export default observer(Error);
