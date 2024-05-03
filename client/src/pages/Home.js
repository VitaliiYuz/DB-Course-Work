import React, { useState, useEffect } from 'react';
import RatingStars from '../components/RatingStars';
import { PRODUCT_ROUTE} from "../utils/consts";
import {useNavigate} from "react-router-dom";
import { Loader } from '../components/ui/Loader';
const Home = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const getProduct = async () => {
        try {
            const token = localStorage.getItem('token');
            const url = `https://localhost:7076/api/Products`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const productsJson = await response.json();
                setProducts(productsJson);
            } else {
                console.error('An error occurred when requesting the server', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred while executing the request', error);
        }
    };

    useEffect(() => {
        getProduct()
            .finally(() => setLoading(false));
    }, []);

    if(loading){
        return <Loader/>
    }
    
    

    const extractCategories = () => {
        const categoriesSet = new Set();
        products.forEach(product => categoriesSet.add(product.category.name));
        return Array.from(categoriesSet);
    };

    const renderProductsByCategory = () => {
        const categories = extractCategories();

        return categories.map(categoryName => {
            const categoryProducts = products.filter(product => product.category.name === categoryName);
            const sortedProducts = categoryProducts.sort((a, b) => b.averageRating - a.averageRating);
            const limitedProducts = sortedProducts.slice(0, 4); // Обмежуємо кількість продуктів до максимуму в 4

            return (
                <div key={categoryName} >

                    <div>
                        <div className="bg-white">

                            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                                <h1 className="text-4xl font-bold tracking-tight text-gray-900">{categoryName}</h1>
                                <div className="mt-2 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
                                    {limitedProducts.map(product => (
                                        <div key={product.id} className="group relative" onClick={() => navigate(PRODUCT_ROUTE + '/' + product.id)}>
                                            <div className="aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
                                                <img
                                                    src={product.imageUrl}
                                                    className="h-full w-full object-cover object-center lg:h-full lg:w-full"
                                                />
                                            </div>
                                            <div className="mt-4 flex justify-between">
                                                <div>
                                                    <h3 className="text-sm text-gray-700">
                                                        <a href={product.href}>
                                                            <span aria-hidden="true" className="absolute inset-0"/>
                                                            {product.name}
                                                        </a>
                                                        <RatingStars className="star_product" averageRating={product.averageRating} />
                                                    </h3>
                                                    <p className="mt-1 text-sm text-gray-500">{product.color}</p>
                                                </div>
                                                <p className="text-sm font-medium text-gray-900">{product.price}</p>
                                            </div>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            );
        });
    };


    return (
        <div>
            {renderProductsByCategory()}
        </div>
    );
};

export default Home;