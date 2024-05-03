import axios from "axios";

const $host = axios.create({
    baseURL: "https://localhost:7076"
})

const $authHost = axios.create({
    baseURL: "https://localhost:7076"
})

const authInterceptor = config => {
    config.headers.authorization = `Bearer ${localStorage.getItem('token')}`
    return config
}

$authHost.interceptors.request.use(authInterceptor)

export {
    $host,
    $authHost
}
