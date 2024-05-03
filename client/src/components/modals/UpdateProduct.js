import React, {Fragment, useEffect, useRef, useState} from 'react';
import {Dialog, Transition} from "@headlessui/react";
import {updateProduct, uploadProductImage} from "../../http/productAPI";
import {useNavigate} from "react-router-dom";

const UpdateProduct = ({open, setOpen, product}) => {
    const cancelButtonRef = useRef(null);
    const [productName, setProductName] = useState(product.name);
    const [discount, setDiscount] = useState(product.discount);
    const [price, setPrice] = useState(product.price);
    const [image, setImage] = useState(null);
    const navigate = useNavigate();

    const editProduct = async () =>{
        try{
            const json = {
                id: product.id,
                name: productName,
                imageUrl: product.imageUrl,
                discount: discount,
                price: price,
                categoryId: product.categoryId,
            }
            await updateProduct(json);
            setOpen(false);
        }catch (e){
            console.error(e);
        }
    }

    const handleFileChange = (e) => {
        setImage(e.target.files[0]);
    }

    const uploadImage = async () =>{
        const formData = new FormData();
        formData.append('file', image);
        try{
            if(image !== null){
                await uploadProductImage(product.id, formData);
            }
            setOpen(false);
        }catch (e){
            console.error(e);
        }
    }

    useEffect(() => {

    }, []);

    return (
        <Transition.Root show={open} as={Fragment}>
            <Dialog as="div" className="relative z-10" initialFocus={cancelButtonRef} onClose={setOpen}>
                <Transition.Child
                    as={Fragment}
                    enter="ease-out duration-300"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in duration-200"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    <div className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
                </Transition.Child>

                <div className="fixed inset-0 z-10 w-screen overflow-y-auto">
                    <div className="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
                        <Transition.Child
                            as={Fragment}
                            enter="ease-out duration-300"
                            enterFrom="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                            enterTo="opacity-100 translate-y-0 sm:scale-100"
                            leave="ease-in duration-200"
                            leaveFrom="opacity-100 translate-y-0 sm:scale-100"
                            leaveTo="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                        >
                            <Dialog.Panel className="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg">
                                <div className="bg-white px-4 pb-4 pt-5 sm:p-6 sm:pb-4">
                                    <div className="sm:flex sm:items-start w-full">
                                        <div className="mt-3 text-center sm:ml-4 sm:mt-0 sm:text-left">
                                            <Dialog.Title as="h3" className="text-2xl font-semibold leading-8 text-gray-900">
                                                Edit product
                                            </Dialog.Title>
                                            <div className="mt-6 ml-20 mr-20">
                                                <div>
                                                    <label htmlFor="file"
                                                           className="block text-sm font-medium leading-6 text-gray-900">
                                                        Upload image
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            id="file"
                                                            name="file"
                                                            type="file"
                                                            required
                                                            className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                                            onChange={(e) => handleFileChange(e)}
                                                        />
                                                    </div>
                                                    <button
                                                        onClick={uploadImage}
                                                        className="inline-flex w-full justify-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-500 sm:w-auto">
                                                        Upload
                                                    </button>
                                                </div>
                                                <div>
                                                    <label htmlFor="productName"
                                                           className="block text-sm font-medium leading-6 text-gray-900">
                                                        Product name
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            id="productName"
                                                            name="productName"
                                                            value={productName}
                                                            required
                                                            className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                                            onChange={(e) => setProductName(e.target.value)}
                                                        />
                                                    </div>
                                                </div>
                                                <div>
                                                    <label htmlFor="discount"
                                                           className="block text-sm font-medium leading-6 text-gray-900">
                                                        Discount
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            id="discount"
                                                            name="discount"
                                                            type="number"
                                                            value={discount}
                                                            required
                                                            className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                                            onChange={(e) => setDiscount(e.target.value)}
                                                        />
                                                    </div>
                                                </div>
                                                <div>
                                                    <label htmlFor="price"
                                                           className="block text-sm font-medium leading-6 text-gray-900">
                                                        Price
                                                    </label>
                                                    <div className="w-full">
                                                        <input
                                                            id="price"
                                                            name="price"
                                                            value={price}
                                                            required
                                                            className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                                            onChange={(e) => setPrice(e.target.value)}
                                                        />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
                                    <button
                                        type="button"
                                        className="inline-flex w-full justify-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-red-500 sm:ml-3 sm:w-auto"
                                        onClick={editProduct}
                                    >
                                        Update
                                    </button>
                                    <button
                                        type="button"
                                        className="mt-3 inline-flex w-full justify-center rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50 sm:mt-0 sm:w-auto"
                                        onClick={() => setOpen(false)}
                                        ref={cancelButtonRef}
                                    >
                                        Cancel
                                    </button>
                                </div>
                            </Dialog.Panel>
                        </Transition.Child>
                    </div>
                </div>
            </Dialog>
        </Transition.Root>
    );
};

export default UpdateProduct;