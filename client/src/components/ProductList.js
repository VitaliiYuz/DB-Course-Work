import React, {useContext} from 'react';
import {Context} from "../index";
import ProductItem from "./ProductItem";

const ProductList = ({products}) => {


    return (
        <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                <div className="mt-2 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
                    {products.map((product) => (
                        <ProductItem key={product.id} product={product}/>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default ProductList;