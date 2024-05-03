import React, { useEffect, useState } from 'react';
import { fetchOrders } from "../http/userAPI";
import { format } from 'date-fns';
import { uk } from 'date-fns/locale';
import '../components/styles/order.css'; // імпорт CSS-файлу
import RatingStars from '../components/RatingStars';

const Order = () => {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        getOrders();
    }, []);

    const getFormattedDate = (date) => {
        let dateObject = new Date(date);
        return format(dateObject, "d MMMM yyyy", { locale: uk });
    }

    const getOrders = async () => {
        try {
            const token = localStorage.getItem('token');
            const url = `https://localhost:7076/api/Orders`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const ordersJson = await response.json();
                setOrders(ordersJson);
            } else {
                console.error('An error occurred when requesting the server', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred while executing the request', error);
        }
    };

    return (
        <div className='orders-container mt-10'>
            {orders.map(order => (
                <div key={order.id} className="order-container">
                    <h3 className="order-id">Order ID: {order.id}</h3>
                    <p className="order-date">Created Date: {getFormattedDate(order.createdDate)}</p>
                    <ul className="order-details-list">
                        {order.details.map(detail => (
                            <li key={detail.id} className="order-detail">
                                <div className="product-image" style={{ backgroundImage: `url(${detail.product.imageUrl})` }}></div>
                                <div className="product-details">
                                    <p className="product-name">Product: {detail.product.name}</p>
                                    <RatingStars className="star_product" averageRating={detail.product.averageRating} />
                                    <p className="quantity">Quantity: {detail.quantity}</p>
                                    <p className="quantity">Category: {detail.category}</p>
                                    <p className="unit-price">Unit Price: {detail.unitPrice}</p>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
            ))}
        </div>
    );
};

export default Order;