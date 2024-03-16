import { createRouter, createWebHashHistory } from 'vue-router';
import ProductsPage from '@/router/pages/ProductsPage.vue';
import ProductPage from '@/router/pages/ProductPage.vue';
import MainPage from '@/router/pages/MainPage.vue';
import NotFound from '@/router/pages/NotFound.vue';
import router from '@/router/router';
import store from '@/store';

export default createRouter({
    routes: [
        { path: '/', component: MainPage },
        { path: '/products', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'productsPage', path: '/products/:page', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'product', path: '/products/productId=:productId', component: ProductPage },
        { name: 'notFound', path: '/notfound', component: NotFound },
    ],
    history: createWebHashHistory(),
})

router.beforeEach((to, from, next) => {
    
    if (to.meta.requiresFiltersLoading) {
        const currentPage = store.getters['productsCatalog/getCurrentPage'];
        const totalPages = store.getters['productsCatalog/getTotalPages'];
        if (to.params.page) {
            store.commit('setCurrentPage', Number(to.params.page));
            store.dispatch('loadAppliedFilters')
        } else {
            to.params.page = '1';
            store.commit('setCurrentPage', Number(to.params.page));
            store.dispatch('loadAppliedFilters')
        }
        if (totalPages > 0 && to.params.page !== undefined) {
            if (currentPage < 1 || currentPage > totalPages || Number(to.params.page) < 1 || Number(to.params.page) > totalPages) router.push('/notfound');
        } 
    }

    next();
});