import React from 'react';
import {format} from 'date-fns';
import {uk} from 'date-fns/locale'

const OrderItem = ({ order }) => {

    const getFormattedDate = (date) =>{
        let dateObject = new Date(date);
        return format(dateObject, "d MMMM yyyy",
            {locale: uk});
    }

    return (
        <>
            {order.details.length > 0
                ?
                <div
                    style={{height:'80px', padding: '15px', borderRadius: '4px'}}
                    className='flex align-items-center bg-gray-200 w-full'>
                    <div>
                        <h2 className='mt-1 max-w-2xl text-sm leading-6 text-gray-500'>Замовлення №{order.id}</h2>
                        <p className='mt-1 max-w-2xl text-sm leading-6 text-gray-500'>{getFormattedDate(order.createdDate)}</p>
                    </div>

                    {
                        order.details.map(detail => (
                            <div key={detail.id} className="w-60">
                                <div
                                    className="overflow-hidden lg:aspect-none group-hover:opacity-75">
                                    <img
                                        src={detail.product.imageUrl}
                                        className="h-full w-full"
                                    />
                                </div>
                            </div>
                        ))
                    }
                </div>
                : ''
            }

        </>
    );
};

export default OrderItem;