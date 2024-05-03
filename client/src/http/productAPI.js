import {$authHost, $host} from "./index";


export const fetchProducts = async () => {
    const {data} = await $host.get('api/products')
    return data
}

export const fetchCategories = async () => {
    const {data} = await $host.get('api/categories', )
    return data
}


export const fetchFeatures = async (typeId, brandId, page, limit= 5) => {
    const {data} = await $host.get('api/device', {params: {
            typeId, brandId, page, limit
        }})
    return data
}

export const fetchOneProduct = async (productId) => {
    const {data} = await $host.get(`api/products/${productId}`);
    return data;
}

export const createCategory = async (name) => {
    const {data} = await $authHost.post('api/categories', {name});
    return data;
}

export const createProduct = async (req) => {
    const {data} = await $authHost.post('api/products', req);
    return data;
}

export const updateProduct = async (req) => {
    await $authHost.put('api/products', req);
}

export const removeProduct = async (id) => {
    await $authHost.delete(`api/products/${id}`);
}

export const uploadProductImage = async (id, req) =>{
    await $authHost.post(`api/products/${id}`, req, {
        headers: {
            'Content-Type': 'multipart/form-data',
        }
    });
}

export const createReview = async (req) => {
    const {data} = await $authHost.post('api/reviews', req);
    return data;
}
