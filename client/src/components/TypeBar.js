import React, { useContext, Fragment, useState, useEffect } from 'react';
import { observer } from "mobx-react-lite";
import { Context } from "../index";
import { Disclosure, Menu, Transition } from '@headlessui/react'
import { ChevronDownIcon, FunnelIcon, MinusIcon, PlusIcon, Squares2X2Icon } from '@heroicons/react/20/solid'
import ProductList from './ProductList';
import CreateCategory from "./modals/CreateCategory";
import {useNavigate} from "react-router-dom";
import {CREATE_PRODUCT_ROUTE} from "../utils/consts";
import { Loader } from './ui/Loader';


function classNames(...classes) {
    return classes.filter(Boolean).join(' ')
}

const TypeBar = observer(() => {
    const [mobileFiltersOpen, setMobileFiltersOpen] = useState(false);
    const { product, user } = useContext(Context);
    const [openCreateCategory, setOpenCreateCategory] = useState(false);
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    const filters = [
        {
            id: 'category',
            name: 'Category',
            options: product.categories,
        }
    ]

    const products = product.products;
    const [filteredProducts, setFilteredProducts] = useState([]);
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [sortOptions, setSortOptions] = useState([
        { id: 1, name: 'Best Rating', href: '#', current: true },
        { id: 2, name: 'Price: Low to High', href: '#', current: false },
        { id: 3, name: 'Price: High to Low', href: '#', current: false },
    ]);

    const [selectedSort, setSelectedSort] = useState(null);

    const handleCategoryChange = (e) => {
        const categoryId = e.target.value;
        const isChecked = e.target.checked;

        if (isChecked) {
            setSelectedCategories([...selectedCategories, Number(categoryId)]);
        }
        else {
            setSelectedCategories(selectedCategories.filter(id => id !== Number(categoryId)));
        }
    }

    const handleSortChange = (name) => {
        if (name !== null) {
            const updatedSortOptions = sortOptions.map(option => ({
                ...option,
                current: option.name === name ? true : false,
            }));
            setSortOptions(updatedSortOptions);
            setSelectedSort(sortOptions.find(option => option.name === name));
        }
    }

    useEffect(() => {
        if (selectedCategories.length > 0) {
            setFilteredProducts(products.filter(
                (product) => selectedCategories.includes(product.category.id)
            ));
        }
        else {
            setFilteredProducts(products);
            setLoading(false);
        }
    }, [products, selectedCategories]);

    useEffect(() => {
        if(selectedSort !== null){
            const sortedProducts = [...filteredProducts].sort((a, b) => {
                if (selectedSort.id === 2 || selectedSort.id === 3) {
                    const priceA = a.price;
                    const priceB = b.price;
                    return selectedSort.id == 2 ? priceA - priceB : priceB - priceA;
                }
                if (selectedSort.id === 1) {
                    const ratingA = a.averageRating;
                    const ratingB = b.averageRating;
                    return ratingB - ratingA;
                }
            });
            setFilteredProducts(sortedProducts);
        }

    }, [selectedSort])

    if(loading){
        return <Loader/>
    }

    return (
        <div className="bg-white">
            <div>
                <main className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                    <div className="flex items-baseline justify-between border-b border-gray-200 pb-6 pt-24">
                        <h1 className="text-4xl font-bold tracking-tight text-gray-900">Products</h1>

                        <div className="flex items-center">
                            <Menu as="div" className="relative inline-block text-left">
                                <div>
                                    <Menu.Button
                                        className="group inline-flex justify-center text-sm font-medium text-gray-700 hover:text-gray-900">
                                        Sort
                                        <ChevronDownIcon
                                            className="-mr-1 ml-1 h-5 w-5 flex-shrink-0 text-gray-400 group-hover:text-gray-500"
                                            aria-hidden="true"
                                        />
                                    </Menu.Button>
                                </div>



                                <Transition
                                    as={Fragment}
                                    enter="transition ease-out duration-100"
                                    enterFrom="transform opacity-0 scale-95"
                                    enterTo="transform opacity-100 scale-100"
                                    leave="transition ease-in duration-75"
                                    leaveFrom="transform opacity-100 scale-100"
                                    leaveTo="transform opacity-0 scale-95"
                                >
                                    <Menu.Items
                                        className="absolute right-0 z-10 mt-2 w-40 origin-top-right rounded-md bg-white shadow-2xl ring-1 ring-black ring-opacity-5 focus:outline-none">
                                        <div className="py-1">
                                            {sortOptions.map((option) => (
                                                <Menu.Item key={option.name}>
                                                    {({ active }) => (
                                                        <a
                                                            onClick={() => handleSortChange(option.name)}
                                                            className={classNames(
                                                                option.current ? 'font-medium text-gray-900' : 'text-gray-500',
                                                                active ? 'bg-gray-100' : '',
                                                                'block px-4 py-2 text-sm'
                                                            )}
                                                        >
                                                            {option.name}
                                                        </a>
                                                    )}
                                                </Menu.Item>
                                            ))}
                                        </div>
                                    </Menu.Items>
                                </Transition>
                            </Menu>

                            <button type="button" className="-m-2 ml-5 p-2 text-gray-400 hover:text-gray-500 sm:ml-7">
                                <span className="sr-only">View grid</span>
                                <Squares2X2Icon className="h-5 w-5" aria-hidden="true" />
                            </button>

                            {user.isAdmin()
                                ? <div className="ml-4 mb-4 space-y-4 flex items-center mt-5">
                                    <button
                                        type="button"
                                        onClick={() => navigate(CREATE_PRODUCT_ROUTE)}
                                        className="block inline-flex items-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                    >
                                        <PlusIcon className="-ml-0.5 mr-1.5 h-5 w-5" />
                                        Add product
                                    </button>
                                </div>
                                : ''
                            }

                            <button
                                type="button"
                                className="-m-2 ml-4 p-2 text-gray-400 hover:text-gray-500 sm:ml-6 lg:hidden"
                                onClick={() => setMobileFiltersOpen(true)}
                            >
                                <span className="sr-only">Filters</span>
                                <FunnelIcon className="h-5 w-5" aria-hidden="true" />
                            </button>
                        </div>
                    </div>

                    <section aria-labelledby="products-heading" className="pb-24 pt-6">
                        <h2 id="products-heading" className="sr-only">
                            Products
                        </h2>

                        <div className="grid grid-cols-1 gap-x-8 gap-y-10 lg:grid-cols-4">
                            {/* Filters */}
                            <form className="hidden lg:block">
                                <h3 className="sr-only">Categories</h3>
                                {filters.map((section) => (
                                    <Disclosure as="div" key={section.id} className="border-b border-gray-200 py-6">
                                        {({ open }) => (
                                            <>
                                                <h3 className="-my-3 flow-root">
                                                    <Disclosure.Button
                                                        className="flex w-full items-center justify-between bg-white py-3 text-sm text-gray-400 hover:text-gray-500">
                                                        <span
                                                            className="font-medium text-gray-900">{section.name}</span>
                                                        <span className="ml-6 flex items-center">
                                                            {open ? (
                                                                <MinusIcon className="h-5 w-5" aria-hidden="true" />
                                                            ) : (
                                                                <PlusIcon className="h-5 w-5" aria-hidden="true" />
                                                            )}
                                                        </span>
                                                    </Disclosure.Button>
                                                </h3>
                                                <Disclosure.Panel className="pt-6">
                                                    <div className="space-y-4">
                                                        {section.options.map((option, optionIdx) => (
                                                            <div key={option.id} className="flex items-center">
                                                                <input
                                                                    id={`filter-${section.id}-${optionIdx}`}
                                                                    name={`${section.id}[]`}
                                                                    defaultValue={option.id}
                                                                    type="checkbox"
                                                                    className="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500"
                                                                    onClick={(e) => handleCategoryChange(e)}
                                                                />
                                                                <label
                                                                    htmlFor={`filter-${section.id}-${optionIdx}`}
                                                                    className="ml-3 text-sm text-gray-600"
                                                                >
                                                                    {option.name}
                                                                </label>
                                                            </div>
                                                        ))}
                                                    </div>
                                                    {user.isAdmin()
                                                        ? <div className="space-y-4 flex items-center mt-5">
                                                            <button
                                                                type="button"
                                                                onClick={() => setOpenCreateCategory(true)}
                                                                className="block inline-flex items-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                                            >
                                                                <PlusIcon className="-ml-0.5 mr-1.5 h-5 w-5" />
                                                                Add category
                                                            </button>
                                                        </div>
                                                        : ''
                                                    }

                                                </Disclosure.Panel>
                                            </>
                                        )}
                                    </Disclosure>
                                ))}
                            </form>

                            {/* Product grid */}
                            <div className="lg:col-span-3">
                                <ProductList products={filteredProducts} />
                            </div>
                        </div>
                    </section>
                </main>
            </div>
            <CreateCategory open={openCreateCategory} setOpen={setOpenCreateCategory}/>
        </div>
    );
});

export default TypeBar;
