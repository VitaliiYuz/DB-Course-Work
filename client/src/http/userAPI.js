import {$authHost, $host} from "./index";
import {jwtDecode} from "jwt-decode";

export const registration = async (req) => {
    const {data} = await $host.post('api/users', req)
    return data
}

export const login = async (email, password) => {
    const {data} = await $host.post('api/authentication/login', { login: email, password: password})
    localStorage.setItem('token', data.token)
    return jwtDecode(data.token)
}

export const check = async () => {
    const {data} = await $authHost.get('api/users' )
    return data;
}

export const createBasket = async () => {
    const {data} = await $authHost.post('api/baskets');
    return data;
}

export const fetchBasket = async () => {
    const {data} = await $authHost.get('api/baskets');
    return data;
}

export const addItemToBasket = async (req) => {
    const {data} = await $authHost.post('api/baskets/items', req);
    return data;
}

export const removeItemFromBasket = async (id) =>{
    const {data} = await $authHost.delete(`api/baskets/items/${id}`);
    return data;
}

export const doCheckout = async () => {
    const {data} = await $authHost.post('api/orders');
    return data;
}

export const fetchOrders = async () => {
    const {data} = await $authHost.get('api/orders');
    return data;
}

export const fetchOrderCountByMonth = async () => {
    const {data} = await $authHost.get('api/orders/count');
    return data;
}

export const fetchSumOrderTotalCountByMonth = async () => {
    const {data} = await $authHost.get('api/orders/total');
    return data;
}

export const fetchAverageOrderTotalByMonth = async () => {
    const {data} = await $authHost.get('api/orders/average');
    return data;
}

export const fetchOrdersFile = async () => {
    const {data} = await $authHost.get('api/orders/last', {
        responseType: 'blob',
    });
    return data;
}