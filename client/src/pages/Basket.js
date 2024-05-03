import {Fragment, useContext, useEffect, useState} from 'react'
import { Dialog, Transition } from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/outline'
import { Context } from '..';
import { useNavigate } from 'react-router-dom';
import {LOGIN_ROUTE, ORDERS_ROUTE} from '../utils/consts';
import {doCheckout, fetchBasket, removeItemFromBasket} from '../http/userAPI';
import { Loader } from '../components/ui/Loader';

const Basket = ({open, setOpen}) => {
    const {user} = useContext(Context);
    const navigate = useNavigate();
    const [items, setItems] = useState([]);
    const [basket, setBasket] = useState({});

    useEffect(() => {
        if(!user.isAuth){
            navigate(LOGIN_ROUTE);

            return;
        }
        try{
            fetchBasket()
            .then(data =>{
                user.setBasket(data);
                user.setBasketTotal(calculateTotal(data));
                setBasket(data);
                setItems(data.items);
            });
        }
        catch(e){
            alert(e);
        }
    }, [open]);

    const removeFromBasket = async (id) =>{
        try{
            console.log(id);
            await removeItemFromBasket(id);
            setItems(items.filter(item => item.id !== id));
        }
        catch (e){
            console.error(e);
        }
    }

    const calculateTotal = (data) =>{
        let total = 0;
        data.items.forEach(item => {
            total += item.quantity * item.product.price;
        });
        return total;
    }

    const checkout = async () =>{
        try{
            await doCheckout();
            setItems({});
            navigate(ORDERS_ROUTE);

            setOpen(false);
        }
        catch (e){
            console.error(e);
        }
    }

    return (
        <Transition.Root show={open} as={Fragment}>
            <Dialog as="div" className="relative z-10" onClose={setOpen}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-in-out duration-500"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in-out duration-500"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    <div className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
                </Transition.Child>

                <div className="fixed inset-0 overflow-hidden">
                    <div className="absolute inset-0 overflow-hidden">
                        <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
                            <Transition.Child
                                as={Fragment}
                                enter="transform transition ease-in-out duration-500 sm:duration-700"
                                enterFrom="translate-x-full"
                                enterTo="translate-x-0"
                                leave="transform transition ease-in-out duration-500 sm:duration-700"
                                leaveFrom="translate-x-0"
                                leaveTo="translate-x-full"
                            >
                                <Dialog.Panel className="pointer-events-auto w-screen max-w-md">
                                    <div className="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                                        <div className="flex-1 overflow-y-auto px-4 py-6 sm:px-6">
                                            <div className="flex items-start justify-between">
                                                <Dialog.Title className="text-lg font-medium text-gray-900">Shopping cart</Dialog.Title>
                                                <div className="ml-3 flex h-7 items-center">
                                                    <button
                                                        type="button"
                                                        className="relative -m-2 p-2 text-gray-400 hover:text-gray-500"
                                                        onClick={() => setOpen(false)}
                                                    >
                                                        <span className="absolute -inset-0.5" />
                                                        <span className="sr-only">Close panel</span>
                                                        <XMarkIcon className="h-6 w-6" aria-hidden="true" />
                                                    </button>
                                                </div>
                                            </div>

                                            <div className="mt-8">
                                                <div className="flow-root">
                                                    <ul role="list" className="-my-6 divide-y divide-gray-200">
                                                        {items.length > 0 ?
                                                        items.map((item) => (
                                                            <li key={item.id} className="flex py-6">
                                                                <div className="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
                                                                    <img
                                                                        src={item.product.imageUrl}
                                                                        className="h-full w-full object-cover object-center"
                                                                    />
                                                                </div>

                                                                <div className="ml-4 flex flex-1 flex-col">
                                                                    <div>
                                                                        <div className="flex justify-between text-base font-medium text-gray-900">
                                                                            <h3>
                                                                                <a>{item.product.name}</a>
                                                                            </h3>
                                                                            <p className="ml-4">{item.product.price} грн</p>
                                                                        </div>
                                                                    </div>
                                                                    <div className="flex flex-1 items-end justify-between text-sm">
                                                                        <p className="text-gray-500">Quantity {item.quantity}</p>

                                                                        <div className="flex">
                                                                            <button
                                                                                onClick={() => removeFromBasket(item.id)}
                                                                                type="button"
                                                                                className="font-medium text-indigo-600 hover:text-indigo-500"
                                                                            >
                                                                                Remove
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </li>
                                                        ))
                                                    : <div className='flex justify-center items-center'>
                                                        <h2>No items</h2>
                                                      </div>
                                                    }
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                                            <div className="flex justify-between text-base font-medium text-gray-900">
                                                <p>Subtotal</p>
                                                <p>{basket.items != null ? 
                                                user.basketTotal
                                                : 0
                                            }</p>
                                            </div>
                                            <p className="mt-0.5 text-sm text-gray-500">Shipping and taxes calculated at checkout.</p>
                                            <div className="mt-6">
                                                <a
                                                    onClick={checkout}
                                                    className="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-indigo-700"
                                                >
                                                    Checkout
                                                </a>
                                            </div>
                                            <div className="mt-6 flex justify-center text-center text-sm text-gray-500">
                                                <p>
                                                    or{' '}
                                                    <button
                                                        type="button"
                                                        className="font-medium text-indigo-600 hover:text-indigo-500"
                                                        onClick={() => setOpen(false)}
                                                    >
                                                        Continue Shopping
                                                        <span aria-hidden="true"> &rarr;</span>
                                                    </button>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </Dialog.Panel>
                            </Transition.Child>
                        </div>
                    </div>
                </div>
            </Dialog>
        </Transition.Root>
    );
};

export default Basket;
