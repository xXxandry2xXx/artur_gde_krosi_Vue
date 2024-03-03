import { createRouter, createWebHashHistory } from 'vue-router';
import ProductsPage from '@/pages/ProductsPage.vue';
import ProductPage from '@/pages/ProductPage.vue';
import MainPage from '@/pages/MainPage.vue';
import components from '../components';

export default createRouter({
    routes: [
        { path: '/', component: MainPage },
        { path: '/products', component: ProductsPage },
        { name: 'productsPage', path: '/products/:page', component: ProductsPage },
        { name: 'productPage', path: '/products/productId=:productId', component: ProductPage },
    ],
    history: createWebHashHistory()
})