import {observer} from "mobx-react-lite";
import {useContext, useEffect, useState} from "react";
import {Context} from "../index";
import {fetchCategories, fetchProducts} from "../http/productAPI";
import TypeBar from "../components/TypeBar";
import { Loader } from "../components/ui/Loader";

const Shop = observer(() => {
    const {product} = useContext(Context);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchCategories().then(data => {
            product.setCategories(data);
        });
        fetchProducts().then(data => {
            product.setProducts(data);
        })
        .finally(() => setLoading(false));
    }, [product.page, product.selectedCategory])

    if(loading){
        return <Loader/>
    }

    return (
        <div>
            <TypeBar/>
        </div>
    );
});

export default Shop;