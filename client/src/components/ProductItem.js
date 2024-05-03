import React from 'react';
import {useNavigate} from "react-router-dom";
import {IMAGES_API_ROUTE, PRODUCT_ROUTE} from "../utils/consts";
import {$host} from "../http";
import RatingStars from './RatingStars';
const ProductItem = ({product}) => {

    const navigate = useNavigate();

    return (
        <div key={product.id} className="group relative" onClick={() => navigate(PRODUCT_ROUTE + '/' + product.id)}>
            <div
                className="aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
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
    );
};

export default ProductItem;