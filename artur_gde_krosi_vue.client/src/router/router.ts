import { createRouter, createWebHashHistory } from 'vue-router';
import ProductsPage from '@/router/pages/ProductsPage.vue';
import ProductPage from '@/router/pages/ProductPage.vue';
import MainPage from '@/router/pages/MainPage.vue';
import NotFound from '@/router/pages/NotFound.vue';
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';

export default createRouter({
    routes: [
        { path: '/', component: MainPage },
        { path: '/products', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'productsPage', path: '/products/:page', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'product', path: '/products/productId=:productId', component: ProductPage, meta: { isProductPage: true } },
        { name: 'notFound', path: '/notfound', component: NotFound },
    ],
    history: createWebHashHistory(),

    scrollBehavior(to, from, savedPosition) {
        if (to.hash) {
            return {
                el: to.hash,
                behavior: 'smooth',
            };
        } else {
            return {
                top: 0,
                behavior: 'smooth'
            };
        }
    },
});