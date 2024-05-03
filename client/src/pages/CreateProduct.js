import React, { Fragment, useEffect, useState } from 'react';
import { createProduct, fetchCategories } from "../http/productAPI";
import { useNavigate } from "react-router-dom";
import { SHOP_ROUTE } from "../utils/consts";
import { Loader } from '../components/ui/Loader';

function classNames(...classes) {
    return classes.filter(Boolean).join(' ')
}

const CreateProduct = () => {
    const [name, setName] = useState("");
    const [discount, setDiscount] = useState("");
    const [price, setPrice] = useState("");
    const [selectedCategory, setSelectedCategory] = useState(1);
    const [categories, setCategories] = useState([]);
    const navigate = useNavigate();

    const [loading, setLoading] = useState(true);

    const handleSelected = (e) => {
        setSelectedCategory(e.target.value);
    }

    useEffect(() => {
        fetchCategories()
            .then(data => {
                setCategories(data);
            })
            .finally(() => setLoading(false))
    }, [])

    if (loading) {
        return <Loader/>
    }

    const addProduct = async () => {
        console.log(name);
        const json = {
            name: name,
            discountInPercent: discount,
            price: price,
            categoryId: selectedCategory,
        }
        console.log(json);
        try {
            await createProduct(json);
            navigate(SHOP_ROUTE);
        }
        catch (e) {
            console.error(e);
        }
    }

    return (
        <>
            <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                    <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                        Create product
                    </h2>
                </div>

                <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
                        <div>
                            <label htmlFor="categoryName"
                                className="block text-sm font-medium leading-6 text-gray-900">
                                Category
                            </label>
                            <div className="w-full">
                                <select
                                    className="w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={handleSelected}>
                                    {categories.map((category) => (
                                        <option key={category.id} value={category.id}>{category.name}</option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div>
                            <label htmlFor="name" className="block text-sm font-medium leading-6 text-gray-900">
                                Product name
                            </label>
                            <div className="mt-2">
                                <input
                                    id="name"
                                    name="name"
                                    required
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </div>
                        </div>

                        <div>
                            <label htmlFor="discount" className="block text-sm font-medium leading-6 text-gray-900">
                                Discount
                            </label>
                            <div className="mt-2">
                                <input
                                    id="discount"
                                    name="discount"
                                    required
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={(e) => setDiscount(e.target.value)}
                                />
                            </div>
                        </div>

                        <div>
                            <label htmlFor="price" className="block text-sm font-medium leading-6 text-gray-900">
                                Price
                            </label>
                            <div className="mt-2">
                                <input
                                    id="price"
                                    name="price"
                                    required
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={(e) => setPrice(e.target.value)}
                                />
                            </div>
                        </div>

                        <div className='mt-5 flex justify-center'>
                            <button
                                className="flex justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                onClick={() => addProduct()}
                            >
                                Create
                            </button>
                            <button
                                className="ml-10 flex justify-center rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50 sm:mt-0 sm:w-auto"
                                onClick={() => navigate(SHOP_ROUTE)}
                            >
                                Cancel
                            </button>
                        </div>
                </div>
            </div>
        </>
    );
};

export default CreateProduct;