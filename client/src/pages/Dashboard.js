import React, { useEffect, useState } from 'react';
import { BarChart } from "@mui/x-charts/BarChart";
import { LineChart } from '@mui/x-charts/LineChart';
import { fetchAverageOrderTotalByMonth, fetchOrderCountByMonth, fetchOrdersFile, fetchSumOrderTotalCountByMonth } from '../http/userAPI';
import { Loader } from '../components/ui/Loader';

const months = [
    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
];


const Dashboard = () => {
    const [orderCountByMonth, setOrderCountByMonth] = useState([]);
    const [averageOrderTotal, setAverageOrderTotal] = useState([]);
    const [sumOrderTotal, setSumOrderTotal] = useState([]);

    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchOrderCountByMonth()
            .then(data => {
                handleResponse(data, setOrderCountByMonth);
            })
            .catch(err =>{
                console.error(err);
            })

        fetchAverageOrderTotalByMonth()
            .then(data => {
                handleResponse(data, setAverageOrderTotal);
            })
            .catch(err =>{
                console.error(err);
            })

        fetchSumOrderTotalCountByMonth()
            .then(data => {
                handleResponse(data, setSumOrderTotal);
            })
            .catch(err =>{
                console.error(err);
            })
            .finally(() => setLoading(false));
    }, [])

    const handleResponse = (data, set) => {
        const values = [];
        Object.entries(data).map(([month, count]) => values.push(count));
        set(values);
    }

    const exportOrders = () => {
        fetchOrdersFile()
            .then(data => {
                const url = window.URL.createObjectURL(new Blob([data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', 'orders.xlsx'); // Ім'я файлу для завантаження

                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            })
    }

    if (loading) {
        return <Loader/>
    }

    return (
        <div className='mx-auto max-w-7xl px-4 sm:px-6 lg:px-8'>
            <div className='flex items-center justify-between mt-10'>
                <h1 className="m-5 text-4xl font-bold tracking-tight text-gray-900">Dashboard</h1>
                <button
                className='flex h-10 justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600'
                onClick={exportOrders}>
                    Export orders for last 30 days
                </button>
            </div>

            <div className='flex flex-wrap justify-center'>
                <div>
                    <BarChart
                        xAxis={[{ scaleType: 'band', data: months }]}
                        series={[{
                            label: 'Кількість замовлень',
                            data: orderCountByMonth
                        }]}
                        width={600}
                        height={300}
                    />
                </div>
                <div>
                    <LineChart
                        xAxis={[{ scaleType: 'point', data: months }]}
                        series={[{
                            label: 'Середній чек замовлення',
                            data: averageOrderTotal,
                            area: true,
                            showMark: false,
                            color: '#59a14f'
                        }]}
                        width={600}
                        height={300}
                    />
                </div>
                <div>
                    <LineChart
                        xAxis={[{ scaleType: 'point', data: months }]}
                        series={[{
                            label: 'Сума замовлень',
                            data: sumOrderTotal,
                            color: '#af7aa1'
                        }]}
                        width={1000}
                        height={500}
                    />
                </div>
            </div>
        </div>
    );
};

export default Dashboard;