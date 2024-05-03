import React, {useContext} from 'react';
import {observer} from "mobx-react-lite";
import {Context} from "../index";

const Pages = observer(() => {
    const {device} = useContext(Context)
    const pageCount = Math.ceil(device.totalCount / device.limit)
    const pages = []

    for (let i = 0; i < pageCount; i++) {
        pages.push(i + 1)
    }

    return (
        <div className="mt-3">
            {pages.map(page =>
                <div
                    key={page}
                    active={device.page === page}
                    onClick={() => device.setPage(page)}
                >
                    {page}
                </div>
            )}
        </div>
    );
});

export default Pages;
