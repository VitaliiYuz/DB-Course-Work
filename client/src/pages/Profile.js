import React, { useEffect, useState } from 'react';
import { format } from 'date-fns';
import { uk } from 'date-fns/locale';
import '../components/styles/user-profile.css';

const Profile = () => {
    const [user, setUser] = useState([]);

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
            const url = `https://localhost:7076/api/Users`;
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const ordersJson = await response.json();
                setUser(ordersJson);
            } else {
                console.error('An error occurred when requesting the server', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred while executing the request', error);
        }
    };

    return (
        <div className="user-info">
            <p><span className="label">Email:</span> {user.email}</p>
            <p><span className="label">Last Name:</span> {user.lastName}</p>
            <p><span className="label">First Name:</span> {user.firstName}</p>
            <p><span className="label">Phone Number:</span> {user.phoneNumber}</p>
        </div>


    );
};

export default Profile;