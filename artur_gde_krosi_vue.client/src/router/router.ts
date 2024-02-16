import { createRouter, createWebHashHistory } from 'vue-router';
import ProductsPage from '@/pages/ProductsPage.vue';
import MainPage from '@/pages/MainPage.vue';
import components from '../components';

export default createRouter({
    routes: [
        { path: '/', component: MainPage },
        { path: '/products', component: ProductsPage }
    ],
    history: createWebHashHistory()
})